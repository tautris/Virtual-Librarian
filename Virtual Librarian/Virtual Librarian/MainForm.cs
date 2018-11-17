using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using VirtualLibrarian.Domain;

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
            try
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
            catch
            {
                this.Hide();
                detectionTimer.Stop();

                if (MessageBox.Show("Camera not found", "Camera msg", MessageBoxButtons.OK) ==DialogResult.OK)
                {
                    this.Close();
                    
                   }
            }
        }

        private void LearnNewFace_Click(object sender, EventArgs e)
        {
            //Fake login 
            //User user = FileReaderWriter.Instance.GetUser(3);
            //Console.WriteLine(user.Id + user.Name + user.Surname + user.CurrentFaculty);
            var user = new User(1, "cuvakas", "ciuvakenas", User.Faculty.CGF);
            new UserProfile(user).Show();
            this.Hide();
        }
        private void LearnFace()
        {

        }

        private void MainFormLogInAdminButton_Click(object sender, EventArgs e)
        {
            new AdminMainForm().Show();
            this.Hide();
        }
    }
}
