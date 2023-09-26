using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Text;
using System.Threading;
using System.Diagnostics.Eventing.Reader;
using System.Linq;

namespace RandContents
{
    public class RandContents : Form
    {
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        Random ran = new System.Random();
        data[] DATA = new data[100];
        string[] str = new string[100];
        string[] gotContents = new string[1000000];
        string dir = @".\\Contents\";
        ComboBox cmb;
        TextBox txt, pt1, pt2, oppt, firstCostTextBox, firstCostPeriodTextBox;
        Label dpRoulette;
        Image im1, im2;
        PictureBox pb;
        Boolean update = false;
        Label lbWarn;
        int opptInput, userNum = -1, fileCount = 0, gc = 0, counter = 0, userCnt = 0, firstCost = 0, firstCostPeriod = 0, deg = 0;

        [STAThread]
        public static void Main()
        {
            // ThreadExceptionイベント・ハンドラを登録する
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            Application.Run(new RandContents());
        }
        public RandContents()
        {
            //".\\Contents\"以下のファイルをすべて取得する
            IEnumerable<string> files =
                Directory.EnumerateFiles(dir, "*", SearchOption.AllDirectories);

            //ファイルを列挙する
            foreach (string f in files)
            {
                str[fileCount] = f;
                Console.WriteLine(str[fileCount]);
                fileCount++;
            }

            Console.WriteLine("fileCount:" + fileCount);
            float fontSize = 20f;
            Label userLabel, label, ptLabel1, ptLabel2, opp, disposeLabel, firstCostLabel, firstCostPeriodLabel;
            Button btn1, btn2, disposeBtn;
            this.Visible = false;
            this.Text = "RandContents";
            this.ClientSize = new Size(1280, 720);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            timer.Interval = 50;
            timer.Start();

            userLabel = new Label()
            {
                Text = "user:",
                Location = new Point(10, 10),
                AutoSize = true
            };
            txt = new TextBox()
            {
                Location = new Point(100, 10),
                Width = 500
            };
            ptLabel1 = new Label()
            {
                Text = "pt:",
                Location = new Point(600, 10),
                AutoSize = true
            };
            pt1 = new TextBox()
            {
                Location = new Point(650, 10),
                Width = 100
            };
            btn1 = new Button()
            {
                Text = "入力確定",
                Location = new Point(750, 35)
            };
            disposeLabel = new Label()
            {
                Text = "生成したコンテンツ・入力データを消去する場合は\nこちらのすべて消去ボタンを押してください．",
                Location = new Point(900, 35),
                AutoSize = true
            };
            label = new Label()
            {
                Text = "ユーザー名を入力してください．既に入力したユーザーは以下のコンボボックスから選択してください．",
                Location = new Point(10, 60),
                AutoSize = true
            };
            cmb = new ComboBox()
            {
                Text = "[ユーザーを選択してください]",
                Location = new Point(10, 100),
                Width = 500
            };
            ptLabel2 = new Label()
            {
                Text = "pt:",
                Location = new Point(600, 100),
                AutoSize = true
            };
            pt2 = new TextBox()
            {
                Location = new Point(650, 100),
                Width = 100
            };
            btn2 = new Button()
            {
                Text = "コンボ確定",
                Location = new Point(750, 125)
            };

            lbWarn = new Label()
            {
                Text = "",
                Location = new Point(10, 150),
                AutoSize = true
            };
            opp = new Label()
            {
                Text = "1回当たりの消費ポイント:",
                Location = new Point(10, 220),
                AutoSize = true
            };
            oppt = new TextBox()
            {
                Location = new Point(350, 220),
                Width = 100
            };
            firstCostLabel = new Label()
            {
                Text = "初回限定価格:",
                Location = new Point(10, 290),
                AutoSize = true
            };
            firstCostTextBox = new TextBox()
            {
                Location = new Point(90, 290),
                Width = 100
            };
            firstCostPeriodLabel = new Label()
            {
                Text = "初回限定回数:",
                Location = new Point(200, 290),
                AutoSize = true
            };
            firstCostPeriodTextBox = new TextBox()
            {
                Location = new Point(280, 290),
                Width = 100
            };
            dpRoulette = new Label()
            {
                Text = "",
                Location = new Point(10, 650),
                AutoSize = true
            };
            disposeBtn = new Button()
            {
                Text = "すべて消去",
                Location = new Point(1000, 100)
            };

            lbWarn.Font = new Font("游ゴシック", (int)(fontSize / 1.5),
            FontStyle.Bold, GraphicsUnit.Point, 128);
            label.Font = new Font("游ゴシック", (int)(fontSize / 1.5),
            FontStyle.Bold, GraphicsUnit.Point, 128);
            userLabel.Font = new Font("游ゴシック", fontSize,
            FontStyle.Bold, GraphicsUnit.Point, 128);
            ptLabel1.Font = new Font("游ゴシック", fontSize,
            FontStyle.Bold, GraphicsUnit.Point, 128);
            ptLabel2.Font = new Font("游ゴシック", fontSize,
            FontStyle.Bold, GraphicsUnit.Point, 128);
            txt.Font = new Font("游ゴシック", fontSize,
            FontStyle.Bold, GraphicsUnit.Point, 128);
            pt1.Font = new Font("游ゴシック", fontSize,
            FontStyle.Bold, GraphicsUnit.Point, 128);
            disposeLabel.Font = new Font("游ゴシック", (int)(fontSize / 2),
            FontStyle.Bold, GraphicsUnit.Point, 128);
            pt2.Font = new Font("游ゴシック", fontSize,
            FontStyle.Bold, GraphicsUnit.Point, 128);
            cmb.Font = new Font("游ゴシック", fontSize,
            FontStyle.Bold, GraphicsUnit.Point, 128);
            opp.Font = new Font("游ゴシック", fontSize,
            FontStyle.Bold, GraphicsUnit.Point, 128);
            oppt.Font = new Font("游ゴシック", fontSize,
            FontStyle.Bold, GraphicsUnit.Point, 128);
            dpRoulette.Font = new Font("游ゴシック", fontSize,
            FontStyle.Bold, GraphicsUnit.Point, 128);

            this.Controls.Add(userLabel);
            this.Controls.Add(txt);
            this.Controls.Add(ptLabel1);
            this.Controls.Add(pt1);
            this.Controls.Add(btn1);
            this.Controls.Add(disposeLabel);
            this.Controls.Add(label);
            this.Controls.Add(cmb);
            this.Controls.Add(ptLabel2);
            this.Controls.Add(pt2);
            this.Controls.Add(btn2);
            this.Controls.Add(lbWarn);
            this.Controls.Add(opp);
            this.Controls.Add(oppt);
            this.Controls.Add(firstCostLabel);
            this.Controls.Add(firstCostTextBox);
            this.Controls.Add(firstCostPeriodLabel);
            this.Controls.Add(firstCostPeriodTextBox);
            this.Controls.Add(dpRoulette);
            this.Controls.Add(disposeBtn);

            im1 = Image.FromFile(@".\\System\Images\a.png");
            im2 = Image.FromFile(@".\\System\Images\b.png");

            btn1.Click += new EventHandler(btClick1);
            btn2.Click += new EventHandler(btClick2);
            disposeBtn.Click += new EventHandler(disposeBtnClick);
            this.Paint += new PaintEventHandler(form_Paint);
            timer.Tick += new EventHandler(timer_Tick);
            if (File.Exists(@".\\data.csv"))
            {
                CSVR();
            }
            else
            {
                File.Create(@".\\data.csv").Close();
                CSVW();
            }
        }

        //テキスト入力確定ボタン
        public void btClick1(object sender, EventArgs e)
        {
            if (Int32.TryParse(oppt.Text, out opptInput))
            {
                if (opptInput > 0 & opptInput <= 1000000)
                {
                    if (Int32.TryParse(pt1.Text, out int pt1Input))
                    {
                        if (pt1Input > 0 & pt1Input <= 1000000)
                        {
                            if (txt.Text != "")
                            {
                                //ファイル名に使用できない文字を取得
                                char[] invalidChars = System.IO.Path.GetInvalidFileNameChars();
                                if (txt.Text.IndexOfAny(invalidChars) < 0)
                                {
                                    Boolean temp = true;
                                    for (int i = 0; i <= userNum; i++)
                                    {
                                        if (DATA[i].user == txt.Text) temp = false;
                                    }
                                    if (temp)
                                    {
                                        userNum++;
                                        userCnt++;
                                        DATA[userNum] = new data();
                                        DATA[userNum].rouletteCount = 0;
                                        DATA[userNum].user = txt.Text;
                                        for (int j = 0; j < 100; j++) DATA[userNum].dt[j] = 1;
                                        DATA[userNum].pt = pt1Input;
                                        DATA[userNum].ptc = pt1Input;
                                        cmb.Items.Add(txt.Text);
                                        txt.Text = "";
                                        pt1.Text = "";
                                        oppt.Text = Convert.ToString(opptInput);
                                        firstCostTextBox.Text = Convert.ToString(firstCost);
                                        firstCostPeriodTextBox.Text = Convert.ToString(firstCostPeriod);
                                        lbWarn.Text = "";
                                        ptCheck();
                                    }
                                    else
                                    {
                                        lbWarn.Text = "すでに入力されたユーザーです．ドロップダウンから確認してください．";
                                    }
                                }
                                else
                                {
                                    lbWarn.Text = "\"\\,/,:,*,?,\",>,<,|\"の記号は使用できません．記号を取り除いてお試しください．";
                                }
                            }
                            else
                            {
                                lbWarn.Text = "ユーザー名を入力してください．";
                            }
                        }
                        else
                        {
                            lbWarn.Text = "上段のポイントには0より大きく1,000,000以下の数値を入力してください．";
                        }
                    }
                    else
                    {
                        lbWarn.Text = ($"\"{pt1.Text}\"は数値とみなせません．常識的な範囲の数値を上段のポイントにご入力ください．");
                    }
                }
                else
                {
                    lbWarn.Text = "1回あたりの消費ポイントには0より大きく1,000,000以下の数値を入力してください．";
                }
            }
            else
            {
                lbWarn.Text = ($"\"{oppt.Text}\"は数値とみなせません．常識的な範囲の数値を1回当たりの消費ポイントにご入力ください．");
            }
        }

        //ドロップダウンリスト確定ボタン
        public void btClick2(object sender, EventArgs e)
        {
            if (!(cmb.SelectedItem == null))
            {
                if (Int32.TryParse(oppt.Text, out opptInput))
                {
                    if (opptInput > 0 & opptInput <= 1000000)
                    {
                        if (Int32.TryParse(pt2.Text, out int pt2Input))
                        {
                            if (pt2Input > 0 & pt2Input <= 1000000)
                            {
                                userNum = cmb.SelectedIndex;
                                lbWarn.Text = "";
                                pt2.Text = "";
                                oppt.Text = Convert.ToString(opptInput);
                                firstCostTextBox.Text = Convert.ToString(firstCost);
                                firstCostPeriodTextBox.Text = Convert.ToString(firstCostPeriod);
                                cmb.Text = "[ユーザーを選択してください]";
                                DATA[userNum].pt += pt2Input;
                                DATA[userNum].ptc += pt2Input;
                                ptCheck();
                            }
                            else
                            {
                                lbWarn.Text = "下段のポイントには0より大きく1,000,000以下の数値を入力してください．";
                            }
                        }
                        else
                        {
                            lbWarn.Text = ($"\"{pt2.Text}\"は数値とみなせません．常識的な範囲の数値を下段のポイントにご入力ください．");
                        }
                    }
                    else
                    {
                        lbWarn.Text = "1回あたりの消費ポイントには0より大きく1,000,000以下の数値を入力してください．";
                    }
                }
                else
                {
                    lbWarn.Text = ($"\"{oppt.Text}\"は数値とみなせません．常識的な範囲の数値を1回当たりの消費ポイントにご入力ください．");
                }
            }
            else
            {
                lbWarn.Text = "コンボボックスからユーザーを選択してください．";
            }
        }
        public void disposeBtnClick(object sender, EventArgs e)
        {
            //メッセージボックスを表示する
            DialogResult result = MessageBox.Show("このアプリを初期化します．よろしいですか？",
                "警告",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button2);

            //何が選択されたか調べる
            if (result == DialogResult.Yes)
            {
                //「はい」が選択された時
                Console.WriteLine("「はい」が選択されました");
                try
                {

                    DirectoryInfo folder = new DirectoryInfo(@".\\Result");
                    DeleteDirectory(folder);
                    Directory.CreateDirectory(@".\\Result");

                    List<string> list = new List<string>();
                    string tep = "";
                    for (int i = 0; i < fileCount; i++)
                    {
                        tep += "," + str[i].Substring(12);
                    }
                    list.Add("user,累計ポイント,残りポイント,抽選回数" + tep);
                    File.Create(@".\\temp.csv").Close();
                    FileStream fs = new FileStream(@".\\temp.csv", FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
                    TextWriter sr = new StreamWriter(fs, Encoding.GetEncoding("shift-jis"));
                    // Read and display lines from the file until the end of
                    // the file is reached.
                    foreach (string line in list)
                    {
                        sr.WriteLine(line);
                    }
                    userNum = -1; gc = 0; counter = 0; userCnt = 0; deg = 0;
                    sr.Dispose();
                    fs.Dispose();
                    FileInfo file = new FileInfo(@".\\data.csv");
                    if ((file.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    {
                        file.Attributes = FileAttributes.Normal;
                    }
                    file.Delete();
                    System.IO.File.Move(@".\\temp.csv", @".\\data.csv");
                    opptInput = 0;
                    firstCost = 0;
                    firstCostPeriod = 0;
                    binW();
                    CSVR();
                    cmb.Items.Clear();
                    DATA.Initialize();

                    lbWarn.Text = "初期化に成功しました．";
                }
                catch
                {
                    lbWarn.Text = "初期化に失敗しました．プログラムによってファイルが開かれている可能性があります．";
                }

            }
            else if (result == DialogResult.No)
            {
                //「いいえ」が選択された時
                Console.WriteLine("「いいえ」が選択されました");
                lbWarn.Text = "初期化がキャンセルされました";
            }
        }

        public static void DeleteDirectory(System.IO.DirectoryInfo hDirectoryInfo)
        {
            // すべてのファイルの読み取り専用属性を解除する
            foreach (System.IO.FileInfo cFileInfo in hDirectoryInfo.GetFiles())
            {
                if ((cFileInfo.Attributes & System.IO.FileAttributes.ReadOnly) == System.IO.FileAttributes.ReadOnly)
                {
                    cFileInfo.Attributes = System.IO.FileAttributes.Normal;
                }
            }

            // サブディレクトリ内の読み取り専用属性を解除する (再帰)
            foreach (System.IO.DirectoryInfo hDirInfo in hDirectoryInfo.GetDirectories())
            {
                DeleteDirectory(hDirInfo);
            }

            // このディレクトリの読み取り専用属性を解除する
            if ((hDirectoryInfo.Attributes & System.IO.FileAttributes.ReadOnly) == System.IO.FileAttributes.ReadOnly)
            {
                hDirectoryInfo.Attributes = System.IO.FileAttributes.Directory;
            }

            // このディレクトリを削除する
            hDirectoryInfo.Delete(true);
        }

        //ポイントチェック
        public void ptCheck()
        {
            Boolean tof = true;
            Console.WriteLine("usrpt:" + DATA[userNum].pt);
            while (ptConsumption() & tof)
            {
                if (rnd_Contents())
                {
                    gc++;
                }
                else
                {
                    tof = false;
                    DATA[userNum].ptc += opptInput;
                }
                Console.WriteLine("usrptc:" + DATA[userNum].ptc);
            }
            Console.WriteLine("userCnt:" + userCnt);
            update = true;
        }
        //pt消費
        public Boolean ptConsumption()
        {
            Boolean tf = false, n = false;
            if (firstCostTextBox.Text == null & firstCostPeriodTextBox.Text == null)
            {
                n = true;
            }
            if (!n)
            {
                if (Int32.TryParse(firstCostTextBox.Text, out firstCost))
                {
                    if (Int32.TryParse(firstCostPeriodTextBox.Text, out firstCostPeriod))
                    {
                        if (DATA[userNum].rouletteCount < firstCostPeriod)
                        {
                            if (DATA[userNum].ptc >= firstCost)
                            {
                                tf = true;
                                Console.WriteLine("usrNum:" + userNum);
                                DATA[userNum].ptc -= firstCost;
                            }
                        }
                        else
                        {
                            if (DATA[userNum].ptc >= opptInput)
                            {
                                tf = true;
                                Console.WriteLine("usrNum:" + userNum);
                                DATA[userNum].ptc -= opptInput;
                            }
                        }
                    }
                    else
                    {
                        lbWarn.Text = "初回限定回数には0より大きく1,000,000以下の数値を入力してください．";
                    }
                }
                else
                {
                    lbWarn.Text = "初回限定価格には0より大きく1,000,000以下の数値を入力してください．";
                }
            }
            else
            {
                if (DATA[userNum].ptc >= opptInput)
                {
                    tf = true;
                    Console.WriteLine("usrNum:" + userNum);
                    DATA[userNum].ptc -= opptInput;
                }
            }




            return tf;
        }
        //ランダム抽出（かぶり無し）
        public Boolean rnd_Contents()
        {
            int count = 0;
            string keep = str[0];
            for (int i = 0; i < fileCount; i++)
            {
                count += DATA[userNum].dt[i];
            }
            if (count > 0)
            {
                int randm = rnd(count);
                for (int i = 0; i < fileCount; i++)
                {
                    if (DATA[userNum].dt[i] == 1)
                    {
                        if (randm == 0)
                        {
                            keep = str[i];
                            DATA[userNum].dt[i] = 0;
                            i = fileCount;
                        }
                        else
                        {
                            randm--;
                        }
                    }
                }
                DATA[userNum].rouletteCount++;
                createDirAndCopyFile(keep, @".\\Result" + "\\" + DATA[userNum].user + "様\\" + Path.GetFileName(keep));
                gotContents[gc] = DATA[userNum].user + "さんが" + DATA[userNum].rouletteCount + "回目の抽選で" + Path.GetFileName(keep) + "を獲得しました！";
                Console.WriteLine(Path.GetFileName($@"'{keep}'") + "がコピーされました");
                return true;
            }
            else
            {
                return false;
            }
        }
        //ファイルの生成・コピー
        static void createDirAndCopyFile(string sourceFullPath, string distFullPath)
        {
            string distDir = Path.GetDirectoryName(distFullPath);
            if (!Directory.Exists(distDir))
            {
                Directory.CreateDirectory(distDir);
            }

            File.Copy(sourceFullPath, distFullPath, true);
        }

        //最大値-1を渡すと乱数生成
        public int rnd(int u)
        {
            int r;
            r = ran.Next(u);
            return r;
        }
        //描画処理
        public void form_Paint(Object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int degp = deg + 60;
            //ラジアン単位に変換
            double d1 = degp / (180 / Math.PI);
            double d2 = (degp + 90) / (180 / Math.PI);
            double d4 = (degp + 270) / (180 / Math.PI);
            //新しい座標位置を計算する
            float px = 1100;
            float py = 500;
            float size = 300;
            float x = px + size * (float)Math.Sin(d1);
            float y = py + size * (float)Math.Cos(d1);
            float x1 = px + size * (float)Math.Sin(d2);
            float y1 = py + size * (float)Math.Cos(d2);
            float x2 = px + size * (float)Math.Sin(d4);
            float y2 = py + size * (float)Math.Cos(d4);
            //PointF配列を作成
            PointF[] destinationPoints = {new PointF(x, y),
                    new PointF(x1, y1),
                    new PointF(x2, y2)};
            //画像を表示
            g.DrawImage(im2, destinationPoints);

            g.DrawImage(im1, 900, 300, 400, 400);
        }
        //CSV読み込み
        public void CSVR()
        {
            binR();
            //try
            //{
                Console.WriteLine("CSVR");
                //読み込みたいCSVファイルのパスを指定して開く
                StreamReader sr = new StreamReader(@".\\data.csv", Encoding.GetEncoding("shift-jis"));
                //先頭行を読み取りしないかどうか
                var isFirstLineSkip = true;
                //末尾まで繰り返す
                while (!sr.EndOfStream)
                {
                    // CSVファイルの一行を読み込む
                    string line = sr.ReadLine();
                    // 読み込んだ一行をカンマ毎に分けて配列に格納する
                    string[] values = line.Split(',');
                    if (line is null) continue;
                    //先頭行は項目名なのでスキップする
                    if (isFirstLineSkip)
                    {
                        isFirstLineSkip = false;
                        continue;
                    }
                    userNum++;
                    userCnt++;
                    DATA[userNum] = new data();
                    DATA[userNum].user = values[0];
                    Int32.TryParse(values[1], out DATA[userNum].pt);
                    Int32.TryParse(values[2], out DATA[userNum].ptc);
                    Int32.TryParse(values[3], out DATA[userNum].rouletteCount);
                    cmb.Items.Add(values[0]);
                    for (int j = 0; j < 100; j++)
                    {
                        Int32.TryParse(values[j + 4], out DATA[userNum].dt[j]);
                    }
                }
                sr.Close();
            /*}
            catch
            {
                lbWarn.Text = "CSVファイルの読み込みに失敗しました．CSVファイルを閉じてからまたこのアプリケーションを再度開いてください．\n" +
                    "セキュリティーソフトによる制限がかけられている可能性もあります．このフォルダ全体を保護の対象から外してください．";
            }*/
        }
        //CSV出力
        [STAThread]
        public void CSVW()
        {
            binW();
            List<string> list = new List<string>();
            string tep = "";
            for (int i = 0; i < fileCount; i++)
            {
                tep += "," + str[i].Substring(12);
            }
            list.Add("user,累計ポイント,残りポイント,抽選回数" + tep);
            for (int i = 0; i < userCnt; i++)
            {

                string tempStr = DATA[i].user + "," + DATA[i].pt + "," + DATA[i].ptc + "," + DATA[i].rouletteCount + ",";
                for (int j = 0; j < 100; j++)
                {
                    tempStr += DATA[i].dt[j];
                    if (j != 99)
                    {
                        tempStr += ",";
                    }
                }
                list.Add(tempStr);
            }
            try
            {
                FileStream fs = new FileStream(@".\\data.csv",
                    FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
                TextWriter sr = new StreamWriter(fs, Encoding.GetEncoding("shift-jis"));
                // Read and display lines from the file until the end of
                // the file is reached.
                foreach (string line in list)
                {
                    sr.WriteLine(line);
                }
                lbWarn.Text = "";
                sr.Dispose();
                fs.Dispose();
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be write:");
                Console.WriteLine(e.Message);
                lbWarn.Text = "data.csvに書き込みできませんでした．そのファイルを開いていないか，またはセキュリティソフトの設定を見直してみてください．\n初回起動時にこのメッセージが出る場合もあります．その場合は必ずアプリケーションを再起動してください．";
            }
        }
        //バイナリファイル読み込み
        public void binR()
        {
            try
            {
                byte[] bOpptInput = File.ReadAllBytes(@".\\System\bin\bOpptInput");
                opptInput = BitConverter.ToInt32(bOpptInput, 0);
                byte[] bFirstCost = File.ReadAllBytes(@".\\System\bin\bFirstCost");
                firstCost = BitConverter.ToInt32(bFirstCost, 0);
                byte[] bFirstCostPeriod = File.ReadAllBytes(@".\\System\bin\bFirstCostPeriod");
                firstCostPeriod = BitConverter.ToInt32(bFirstCostPeriod, 0);
                oppt.Text = Convert.ToString(opptInput);
                firstCostTextBox.Text = Convert.ToString(firstCost);
                firstCostPeriodTextBox.Text = Convert.ToString(firstCostPeriod);

            }
            catch
            {
                lbWarn.Text = "バイナリファイルの読み込みに失敗しました．";
            }
        }
        //バイナリファイル書き込み
        public void binW()
        {
            Console.WriteLine("OpptInput:" + opptInput);
            Console.WriteLine("firstCost:" + firstCost);
            Console.WriteLine("firstCostPeriod:" + firstCostPeriod);
            byte[] bOpptInput = BitConverter.GetBytes(opptInput);
            byte[] bFirstCost = BitConverter.GetBytes(firstCost);
            byte[] bFirstCostPeriod = BitConverter.GetBytes(firstCostPeriod);
            if (!Directory.Exists(@".\\System\bin"))
            {
                Directory.CreateDirectory(@".\\System\bin");
            }
            if (!Directory.Exists(@".\\System\bin\bOpptInput"))
            {
                File.Create(@".\\System\bin\bOpptInput").Close();
            }
            if (!Directory.Exists(@".\\System\bin\bFirstCost"))
            {
                File.Create(@".\\System\bin\bFirstCost").Close();
            }
            if (!Directory.Exists(@".\\System\bin\bFirstCostPeriod"))
            {
                File.Create(@".\\System\bin\bFirstCostPeriod").Close();
            }
            File.WriteAllBytes(@".\\System\bin\bOpptInput", bOpptInput);
            File.WriteAllBytes(@".\\System\bin\bFirstCost", bFirstCost);
            File.WriteAllBytes(@".\\System\bin\bFirstCostPeriod", bFirstCostPeriod);

        }

        //ティック処理
        public void timer_Tick(object sender, EventArgs e)
        {
            Invalidate();
            if (this.Visible == false)
            {
                this.Visible = true;
            }
            if (gc > 0)
            {
                if (deg < 359)
                {
                    deg += 5;
                }
                else
                {
                    deg = 0;
                    dpRoulette.Text = gotContents[0];
                    for (int i = 0; i <= gc; i++)
                    {
                        gotContents[i] = gotContents[i + 1];
                    }
                    gc--;
                    if (gc == 0)
                    {
                        counter = 100;
                    }
                }
            }
            else
            {
                if (counter > 0)
                {
                    counter--;
                    if (counter == 0)
                    {
                        dpRoulette.Text = "";
                    }
                }
            }
            if (update == true)
            {
                //csvへの情報の書き出し
                CSVW();
                update = false;
            }
        }
        // （Windowsアプリケーション用）
        public static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Console.WriteLine(e.Exception + "不明なエラーが発生しました．詳しくはたんたるそーすへお問い合わせください．");
        }
    }

}