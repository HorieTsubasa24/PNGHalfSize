using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PNGResize
{
    public partial class Form1 : Form
    {
        public string[] pngFileNames;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //OpenFileDialogクラスのインスタンスを作成
            OpenFileDialog ofd = new OpenFileDialog();

            //はじめのファイル名を指定する
            //はじめに「ファイル名」で表示される文字列を指定する
            ofd.FileName = "";
            //はじめに表示されるフォルダを指定する
            //指定しない（空の文字列）の時は、現在のディレクトリが表示される
            ofd.InitialDirectory = "";
            //[ファイルの種類]に表示される選択肢を指定する
            //指定しないとすべてのファイルが表示される
            ofd.Filter = "pngファイル(*.png*)|*.png;";
            //[ファイルの種類]ではじめに選択されるものを指定する
            //0番目の「pngファイル」が選択されているようにする
            ofd.FilterIndex = 0;
            //タイトルを設定する
            ofd.Title = "開く画像を選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            ofd.RestoreDirectory = true;
            //存在しないファイルの名前が指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            ofd.CheckFileExists = true;
            //存在しないパスが指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            ofd.CheckPathExists = true;

            //複数のファイルを選択できるようにする
            ofd.Multiselect = true;
            
            //ダイアログを表示する
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //OKボタンがクリックされたとき、選択されたファイル名を表示する
                Console.WriteLine(ofd.FileNames);
                for(int i = 0; i < ofd.FileNames.Length; i++)
                    textBox1.Text += ofd.FileNames[i] + ",";
                pngFileNames = ofd.FileNames;
            }
        }

        /// <summary>
        /// 変換
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            string[] paths = pngFileNames;
            if (paths == null)
                return;

            pngFileNames = new string[0];
            textBox1.Text = "";

            for(int i = 0; i < paths.Length; i++)
            {
                BitMapSave(paths[i]);
                textBox1.Text += "変換" + paths[i] + "\n";
            }
        }

        private void BitMapSave(string path)
        {
            //存在する？
            if (path == null || !File.Exists(path))
            {
                MessageBox.Show(path + "はありません！");
                return;
            };

            //元ファイル
            string strPic = path;

            string[] strs = strPic.Split('\\');

            //名前のみ 
            string name = strs[strs.Length - 1];

            //保存先ファイル
            string strNewPic = "";
            for (int i = 0; i < strs.Length - 1; i++)
            {
                strNewPic += strs[i];
                strNewPic += '\\';
            }
            string folder = strNewPic += "SaveFiles";
            strNewPic = folder + "\\" + name;

            Bitmap bmpSrc = new Bitmap(@strPic);
            //元画像の縦横サイズを調べる
            int width = bmpSrc.Width;
            int height = bmpSrc.Height;

            if (!File.Exists(folder))
                Directory.CreateDirectory(folder);

            //元画像の縦横サイズを半分にする
            Bitmap bmpSrcHalf = new Bitmap(bmpSrc, width / 2, height / 2);


            //フォーマットをJPGに指定して名前を変えて保存、
            //デフォルトでSaveを実行するとPNGフォーマットになってしまう。
            bmpSrcHalf.Save(strNewPic, ImageFormat.Png);

            bmpSrc.Dispose();
            bmpSrcHalf.Dispose();
        }
    }
}
