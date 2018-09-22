using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Virtual_Librarian
{
    public partial class MainForm : Form
    {
        VideoCapture capture;
        private CascadeClassifier cascadeClassifier;
        Timer timer;

        public MainForm()
        {
            InitializeComponent();
            Window_Loaded();
        }

        private void Window_Loaded()
        {
            capture = new VideoCapture();
            cascadeClassifier = new CascadeClassifier(@"haarcascade_frontalface_default.xml");
            timer = new Timer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = 1;
            timer.Start();
        }

        void timerTick(object sender, EventArgs e)
        {
            Image<Bgr, Byte> currentFrame = capture.QueryFrame().ToImage<Bgr, Byte>();

            if (currentFrame != null)
            {
                Image<Gray, Byte> grayFrame = currentFrame.Convert<Gray, Byte>();
                var detectedFaces = cascadeClassifier.DetectMultiScale(grayFrame, 1.1, 10, Size.Empty);

                foreach (var face in detectedFaces)
                    currentFrame.Draw(face, new Bgr(0, double.MaxValue, 0), 3);
                imgCamUser.Image = currentFrame;
            }
        }

        private void toggleDetection_Click(object sender, EventArgs e)
        {
            // TODO Add some switching between detections if needed
        }

    }
}
