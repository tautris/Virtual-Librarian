using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Virtual_Librarian
{
    public partial class MainForm : Form
    {
        private VideoCapture capture;
        private CascadeClassifier cascadeClassifier;
        private Timer detectionTimer;
        private static Rectangle[] detectedFaces;

        public MainForm()
        {
            InitializeComponent();
            MainFormLoaded();
        }

        private void MainFormLoaded()
        {
            capture = new VideoCapture();
            //Classifier to detect faces from opencv with path bin/debug
            string projectDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            cascadeClassifier = new CascadeClassifier(projectDir + @"\CV\haarcascade_frontalface_default.xml");
            detectionTimer = new Timer();
            detectionTimer.Tick += new EventHandler(DetectFaces);
            detectionTimer.Interval = 1;
            detectionTimer.Start();
        }

        private void DetectFaces(object sender, EventArgs e)
        {
            Image<Bgr, Byte> currentFrame = capture.QueryFrame().ToImage<Bgr, Byte>();

            if (currentFrame != null)
            {
                Image<Gray, Byte> grayFrame = currentFrame.Convert<Gray, Byte>();
                // Face detections happens here by giving the classifier gray image
                detectedFaces = cascadeClassifier.DetectMultiScale(grayFrame, 1.1, 10, Size.Empty);

                foreach (var face in detectedFaces)
                    // Draws rectangles in frame around faces
                    currentFrame.Draw(face, new Bgr(0, double.MaxValue, 0), 3);
                //Returns the frame with drawn rectangles to live feed in Form
                imgCamUser.Image = currentFrame;
            }
        }

        private void LearnNewFace_Click(object sender, EventArgs e)
        {
            Profile f2 = new Profile();
            f2.Show();
            this.Hide();
         
        }
        private void LearnFace()
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void imgCamUser_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
