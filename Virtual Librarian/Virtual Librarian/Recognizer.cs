using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Virtual_Librarian
{
    class RecognizerEngine
    {
        private FaceRecognizer faceRecognizer;
        private readonly string recognizerFilePath;

        public RecognizerEngine(string recognizerFilePath)
        {
            this.recognizerFilePath = recognizerFilePath;

            this.faceRecognizer = new EigenFaceRecognizer(80, double.PositiveInfinity);
        }

        public void TrainRecognizerMany(List<Image<Gray, Byte>> imageList, string[] identifiers) // image list of same user ( for better accuracy)
        {

            if (imageList != null)
            {
                var faceImages = new Image<Gray, byte>[imageList.Count];
                var faceLabels = new int[imageList.Count];
                for (int i = 0; i < imageList.Count(); i++)
                {
                    faceImages[i] = imageList[i].Resize(100, 100, Inter.Cubic);
                    faceLabels[i] = identifiers[i].GetHashCode();
                }
                faceRecognizer.Train(faceImages, faceLabels);
                faceRecognizer.Write(recognizerFilePath);
            }

        }

        public void TrainRecognizerSingle(List<Image<Gray, Byte>> imageList, string identifier) // image list of same user ( for better accuracy)
        {
            string[] arrOfSameVal = new string[imageList.Count];
            for (int i = 0; i < imageList.Count; i++)
            {
                arrOfSameVal[i] = identifier;
            }

            TrainRecognizerMany(imageList, arrOfSameVal);

        }

        public void LoadRecognizerData()
        {
            faceRecognizer.Read(recognizerFilePath);
        }

        public bool RecognizeUser(Image<Gray, Byte> userImage, string user)
        {
            faceRecognizer.Read(recognizerFilePath);

            var result = faceRecognizer.Predict(userImage.Resize(100, 100, Inter.Cubic));
            if (result.Label == user.GetHashCode())
            {
                return true;
            }

            return false;
        }
    }
}