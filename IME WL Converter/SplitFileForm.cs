using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Studyzy.IMEWLConverter
{
    public partial class SplitFileForm : Form
    {
        public SplitFileForm()
        {
            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.txbFilePath.Text = openFileDialog1.FileName;
            }
        }

        private void btnSplit_Click(object sender, EventArgs e)
        {
            if (txbFilePath.Text == "")
            {
                MessageBox.Show("请先选择要分割的文件");
                return;
            }
            if (!File.Exists(txbFilePath.Text))
            {
                MessageBox.Show(txbFilePath.Text+ "，该文件不存在");
                return;
            }
            if(rbtnSplitByLine.Checked)
            {
                SplitFileByLine( (int)numdMaxLine.Value);
            }
            else if (rbtnSplitBySize.Checked)
            {
                SplitFileBySize((int) numdMaxSize.Value);
            }
            else
            {
                SplitFileByLength((int) numdMaxLength.Value);
            }
            MessageBox.Show("恭喜你，文件分割完成!");
        }
        private void SplitFileByLine(int maxLine)
        {
         
            Encoding encoding=null;
            var str = FileOperationHelper.ReadFileContent(txbFilePath.Text,ref encoding,Encoding.UTF8);
            string splitLineChar = "\r\n";
            if (str.IndexOf(splitLineChar) < 0)
            {
                if (str.IndexOf('\r') > 0)
                {
                    splitLineChar = "\r";
                }
                else if (str.IndexOf('\n') > 0)
                {
                    splitLineChar = "\n";
                }
                else
                {
                    MessageBox.Show("不能找到行分隔符");
                    return;
                }
            }
            var list = str.Split(new string[] {splitLineChar}, StringSplitOptions.RemoveEmptyEntries);

            StringBuilder fileContent = new StringBuilder();
            int fileIndex = 1;
            for (var i = 0; i < list.Length; i++)
            {
                if (i % maxLine == 0)
                {
                    if (i != 0)
                    {
                        FileOperationHelper.WriteFile(GetWriteFilePath(fileIndex++), encoding,fileContent.ToString());
                        fileContent=new StringBuilder();
                    }
                }
                fileContent.Append(list[i]);
                fileContent.Append(splitLineChar);
            }
        }

        private void SplitFileBySize(int maxSize)
        {
            var encoding = FileOperationHelper.GetEncodingType(txbFilePath.Text);


            int fileIndex = 1;
            int size = (maxSize-10)*1024;//10K的Buffer
            FileStream inFile = new FileStream(txbFilePath.Text, FileMode.Open, FileAccess.Read);

            do
            {
                FileStream outFile = new FileStream(GetWriteFilePath(fileIndex++), FileMode.OpenOrCreate,
                                                    FileAccess.Write);
                if (fileIndex != 2)//不是第一个文件，那么就要写文件头
                {
                    FileOperationHelper.WriteFileHeader(outFile,encoding);
                }
                int data = 0;
                byte[] buffer = new byte[size];
                if ((data = inFile.Read(buffer, 0, size)) > 0)
                {
                    outFile.Write(buffer, 0, data);
                    bool hasContent = true;
                    do
                    {
                        var b = inFile.ReadByte();
                        if (b == 0xA || b == 0xD)
                        {
                            ReadToNextLine(inFile);
                           
                               hasContent=false;
                          
                        }
                        if (b != -1)//文件已经读完
                        {
                            outFile.WriteByte((byte)b);
                        }
                        else
                        {
                            hasContent = false;
                        }
                    } while (hasContent);
                }
                outFile.Close();
                
            } while (inFile.Position != inFile.Length);
            inFile.Close();
        }
        private bool ReadToNextLine(FileStream fs)
        {
            do
            {
                byte b = (byte)fs.ReadByte();
                if (b == -1)
                {
                    return false;
                }
                if (b != 0xA && b != 0xD &&b!=0)
                {
                    fs.Position--;
                    return true;
                }
            } while (true);
        }


        private void SplitFileByLength(int length)
        {
            Encoding encoding = null;
            length = length - 100;//100个字的Buffer
            var str = FileOperationHelper.ReadFileContent(txbFilePath.Text, ref encoding, Encoding.UTF8);
            int fileIndex = 1;
            do
            {
                if (str.Length == 0)
                {
                    break;
                }
                var content = str.Substring(0, Math.Min(str.Length,length));
                str = str.Substring(content.Length);

                var i = Math.Min(str.IndexOf('\r'), str.IndexOf('\n'));
                if (i != -1)
                {
                    content += str.Substring(0, i + 2);
                    str = str.Substring(i + 2);
                }
                FileOperationHelper.WriteFile(GetWriteFilePath(fileIndex++), encoding, content);
            } while (true);
        }
        private string GetWriteFilePath(int i)
        {
            string path = txbFilePath.Text;
            return Path.GetDirectoryName(path)+"\\"+ Path.GetFileNameWithoutExtension(path)+string.Format("{0}",i)+Path.GetExtension(path);
        }
    }
}
