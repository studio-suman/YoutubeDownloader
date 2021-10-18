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

        private void button1_Click(object sender, EventArgs e)
        {
            string path = OpenFolder();
            if(checkBox2.Checked)
            {

                SaveMP3(path, textBox1.Text, "MyPath");
            }

            if (checkBox1.Checked)
            {

                SaveAll(path, textBox1.Text);
            }

        }

        // To Save Audio Only
        private void SaveMP3(string SaveToFolder, string VideoURL, string MP3Name)
        {
            var source = @SaveToFolder;
            var youtube = YouTube.Default;
            var vid = youtube.GetVideo(VideoURL);
            File.WriteAllBytes(source + vid.FullName, vid.GetBytes());

            var inputFile = new MediaFile { Filename = source + vid.FullName };
            var outputFile = new MediaFile { Filename = $"{MP3Name}.mp3" };

            using (var engine = new Engine())
            {
                engine.GetMetadata(inputFile);

                engine.Convert(inputFile, outputFile);
            }
        }
        
        // To Save Video Only
        private void SaveAll(string SaveToFolder, string VideoURL)
        {
            var source = @SaveToFolder;
            var youtube = YouTube.Default;
            var vid = youtube.GetVideo(VideoURL);
            File.WriteAllBytes(source + vid.FullName, vid.GetBytes());

            var inputFile = new MediaFile { Filename = source + vid.FullName };
            var outputFile = new MediaFile { Filename = vid.FullName + ".mp4" };

            using (var engine = new Engine())
            {
                engine.GetMetadata(inputFile);

                engine.Convert(inputFile, outputFile);
            }
        }

        // Bring up a dialog to chose a folder path in which to open or save a file.
        private string OpenFolder()
        {
            var folderBrowserDialog1 = new FolderBrowserDialog();

            // Show the FolderBrowserDialog.
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string folderName = folderBrowserDialog1.SelectedPath;

                return folderName;

            }

            string folderpath = folderBrowserDialog1.SelectedPath;

            return folderpath;
        }
    }

    
   
}
