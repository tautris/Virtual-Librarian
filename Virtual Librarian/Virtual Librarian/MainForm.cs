using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Virtual_Librarian
{
    public partial class MainForm : Form
    {
        private VideoCapture capture;
        private CascadeClassifier cascadeClassifier;
        private Timer timer;

        public MainForm()
        {
            InitializeComponent();
            MainFormLoaded();
        }

        private void MainFormLoaded()
        {
            capture = new VideoCapture();
            //Classifier to detect faces from opencv with path in bin/debug
            cascadeClassifier = new CascadeClassifier(@"haarcascade_frontalface_default.xml");
            timer = new Timer();
            timer.Tick += new EventHandler(detectFaces);
            timer.Interval = 1;
            timer.Start();
        }

        private void detectFaces(object sender, EventArgs e)
        {
            Image<Bgr, Byte> currentFrame = capture.QueryFrame().ToImage<Bgr, Byte>();

            if (currentFrame != null)
            {
                Image<Gray, Byte> grayFrame = currentFrame.Convert<Gray, Byte>();
                // Face detections happens here by giving the classifier gray image
                var detectedFaces = cascadeClassifier.DetectMultiScale(grayFrame, 1.1, 10, Size.Empty);

                foreach (var face in detectedFaces)
                    //Draws rectangles around faces
                    currentFrame.Draw(face, new Bgr(0, double.MaxValue, 0), 3);
                //Returns the image with drawn rectangles to live feed in Form
                imgCamUser.Image = currentFrame;
            }
        }

        private void learnNewFace_Click(object sender, EventArgs e)
        {

        }

    }
}
