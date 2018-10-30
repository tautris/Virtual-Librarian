using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Virtual_Librarian
{
    class Recognizer
    {
        private FaceRecognizer faceRecognizer;
        private readonly string recognizerFilePath;

        public Recognizer(string recognizerFilePath)
        {
            this.recognizerFilePath = recognizerFilePath;

            faceRecognizer = new EigenFaceRecognizer(80, double.PositiveInfinity);
        }

        public void TrainRecognizer(List<Image<Gray, Byte>> imageList, int[] ids) // image list of same user ( for better accuracy)
        {

            if (imageList != null)
            {
                var faceImages = new Image<Gray, byte>[imageList.Count];
                var faceLabels = new int[imageList.Count];
                for (int i = 0; i < imageList.Count(); i++)
                {
                    faceImages[i] = imageList[i].Resize(100, 100, Inter.Cubic);
                    faceLabels[i] = ids[i];
                }
                faceRecognizer.Train(faceImages, faceLabels);
                faceRecognizer.Save(recognizerFilePath);
            }

        }

        public void TrainRecognizer(List<Image<Gray, Byte>> imageList, int id) // image list of same user ( for better accuracy)
        {
            int[] arrOfSameVal = new int[imageList.Count];
            for (int i = 0; i < imageList.Count; i++)
            {
                arrOfSameVal[i] = id;
            }

            TrainRecognizer(imageList, arrOfSameVal);

        }

        public void TrainRecognizer(List<Image<Gray, Byte>> imageList, User user) // image list of same user ( for better accuracy)
        {
            TrainRecognizer(imageList, user.Id);

        }

        public void LoadRecognizerData()
        {
            faceRecognizer.Load(recognizerFilePath);
        }

        public bool RecognizeUser(Image<Gray, Byte> userImage, User user)
        {
            faceRecognizer.Load(recognizerFilePath);

            var result = faceRecognizer.Predict(userImage.Resize(100, 100, Inter.Cubic));
            if (result.Label == user.Id)
            {
                return true;
            }

            return false;
        }

        public User TryAllUsersRecognize(List<User> users, Image<Gray, Byte> inputImage)
        {
            foreach (User user in users)
            {
                if (RecognizeUser(inputImage, user))
                {
                    return user;
                }
            }
            return null;
        }

        public User TryAllUsersRecognize(List<User> users, List<Image<Gray, Byte>> inputImages)
        {
            User user;
            foreach (var image in inputImages)
            {
                if ((user = TryAllUsersRecognize(users, image)) != null)
                {
                    return user;
                }
            }
            return null;
        }
    }
}