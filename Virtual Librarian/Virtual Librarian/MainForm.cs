using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
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
        private static List<Image<Gray, Byte>> detectedFacesImages;
        private static Object imageLock;
        private Recognizer recognizer;

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
            recognizer = new Recognizer(projectDir + @"\CV\facesRecognizer");
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
                    detectedFaces = cascadeClassifier.DetectMultiScale(grayFrame, 1.1, 10, Size.Empty, Size.Empty);

                    foreach (var face in detectedFaces)
                    {
                        currentFrame.Draw(face, new Bgr(0, double.MaxValue, 0), 3);
                    }

                    lock (imageLock)
                    {
                        detectedFacesImages.Clear();
                        foreach (var face in detectedFaces)
                        {
                            grayFrame.ROI = face;
                            detectedFacesImages.Add(grayFrame.Copy());
                        }
                    }

                    //Returns the frame with drawn rectangles to live feed in Form
                    imgCamUser.Image = currentFrame;
                }

            }
            catch
            {
                Hide();
                detectionTimer.Stop();

                if (MessageBox.Show("Camera not found", "Camera msg", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    Close();

                }
            }
        }

        private void LearnNewFace_Click(object sender, EventArgs e)
        {
            //Fake login 
            List<User> userList = FileReaderWriter.Instance.GetUsers();
            List<Image<Gray, Byte>> detectedImages;

            lock (imageLock)
            {
                detectedImages = new List<Image<Gray, byte>>(detectedFacesImages);
            }

            User user = recognizer.TryAllUsersRecognize(userList, detectedImages);
            //Console.WriteLine(user.Id + user.Name + user.Surname + user.CurrentFaculty);

            if (user != null)
            {
                new UserProfile(user).Show();
                Hide();
            }
            else
            {
                MessageBox.Show("User not found", "pls get out", MessageBoxButtons.OK);
            }

        }
        private void LearnFace(User user, List<Image<Gray, Byte>> imagesOfUser)
        {
            recognizer.TrainRecognizer(imagesOfUser, user);
        }

        private void MainFormLogInAdminButton_Click(object sender, EventArgs e)
        {
            new AdminMainForm().Show();
            Hide();
        }
    }
}
