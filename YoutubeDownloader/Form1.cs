using MediaToolkit;
using MediaToolkit.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoLibrary;

namespace YoutubeDownloader
{
    public partial class Utube1 : Form
    {
        
        

        public Utube1()
        {
            InitializeComponent();
            

        }

     


        private async void button1_Click(object sender, EventArgs e)
        {
            // Bring up a dialog to chose a folder path in which to open or save a file.

           using (FolderBrowserDialog fbd = new FolderBrowserDialog() { Description = "Select File Path to Save:"})
            {
                if(fbd.ShowDialog()==DialogResult.OK)
                {
                    MessageBox.Show("Incorrect Path....", "File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    var yt = YouTube.Default;
                    var video = await yt.GetVideoAsync(textBox1.Text);
                    File.WriteAllBytes(fbd.SelectedPath+@"\"+video.FullName, await video.GetBytesAsync());

                    var inputfile = new MediaFile { Filename = fbd.SelectedPath + @"\" + video.FullName };
                    var outputfile = new MediaFile { Filename = $"{fbd.SelectedPath + @"\" + video.FullName}.mp3" };


                    using (var enging = new Engine())
                    {
                        enging.GetMetadata(inputfile);
                        enging.Convert(inputfile, outputfile);

                    }
                    if(checkBox1.Checked)
                    {
                        File.Delete($"{fbd.SelectedPath + @"\" + video.FullName}.mp3");
                    }
                    else
                    {
                        File.Delete(fbd.SelectedPath + @"\" + video.FullName);
                    }
                    progressBar1.Value = 100;
                    label2.Text = "100%"
                    MessageBox.Show("File Dowloaded Successfully", "Confirmation", MessageBoxButtons.OK);

                    

                }
                else
                {
                    MessageBox.Show("Incorrect Path....", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }

        }
    }

    
   
}
