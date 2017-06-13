using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using OpenCvSharp;
using OpenCvSharp.Blob;
using OpenCvSharp.Extensions;
using OpenCvSharp.UserInterface;
using OpenCvSharp.XFeatures2D;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var img1 = new Mat(@"..\..\..\images\1.png", ImreadModes.GrayScale);
            var img2 = new Mat(@"..\..\..\images\2.png", ImreadModes.GrayScale);

            Mat dst1 = SurfFunc(img1, img2);

            pictureBox1.Image = dst1.ToBitmap();
        }

        public Mat SurfFunc(Mat img1, Mat img2)
        {
            // detecting keypoints
            // FastFeatureDetector, StarDetector, SIFT, SURF, ORB, BRISK, MSER, GFTTDetector, DenseFeatureDetector, SimpleBlobDetector
            // SURF = Speeded Up Robust Features
            var detector = SURF.Create(hessianThreshold: 400, nOctaves: 4, nOctaveLayers: 2); //A good default value could be from 300 to 500, depending from the image contrast.
            var pt1 = detector.Detect(img1);
            var pt2 = detector.Detect(img2);

            // computing descriptors, BRIEF, FREAK
            // BRIEF = Binary Robust Independent Elementary Features
            var extractor = FREAK.Create();
            Mat desc1 = new Mat();
            Mat desc2 = new Mat();
            extractor.Compute(img1, ref pt1, desc1);
            extractor.Compute(img2, ref pt2, desc2);

            // matching descriptors
            var matcher = new BFMatcher();
            var matches = matcher.Match(desc1, desc2);

            // Mat hv = Cv2.FindHomography(img1, img2, HomographyMethods.Ransac);

            FlannBasedMatcher matcher1 = new FlannBasedMatcher();

            double max_dist = 0; double min_dist = 200;

            for (int i = 0; i < desc1.Rows; i++)
            {
                double dist = matches[i].Distance;
                if (dist < min_dist) min_dist = dist;
                if (dist > max_dist) max_dist = dist;
            }

            List<DMatch> good_matches = new List<DMatch>();

            for (int i = 0; i < desc1.Rows; i++)
            {
                if (matches[i].Distance <= Math.Max(2 * min_dist, 0.02))
                {
                    good_matches.Add(matches[i]);
                }
            }

            Mat img_matches = new Mat();

            Cv2.DrawMatches(img1, pt1, img2, pt2,
            good_matches, img_matches, Scalar.All(-1), Scalar.All(-1),
            new List<byte>(), DrawMatchesFlags.Default);

            var imgMatches = new Mat();
            Cv2.DrawMatches(img1, pt1, img2, pt2, matches, imgMatches);

            return imgMatches; //imgMatches img_matches


        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int V = trackBar1.Value;
            switch (V)
            {
                case 1:
                    {
                        var img1 = new Mat(@"..\..\..\images\1.png", ImreadModes.GrayScale);
                        var img2 = new Mat(@"..\..\..\images\2.png", ImreadModes.GrayScale);

                        Mat dst1 = SurfFunc(img1, img2);
                        pictureBox1.Image = dst1.ToBitmap();
                        break;
                    }
                case 2:
                    {
                        var img1 = new Mat(@"..\..\..\images\3.png", ImreadModes.GrayScale);
                        var img2 = new Mat(@"..\..\..\images\4.png", ImreadModes.GrayScale);

                        Mat dst1 = SurfFunc(img1, img2);
                        pictureBox1.Image = dst1.ToBitmap();
                        break;
                    }
                case 3:
                    {
                        var img1 = new Mat(@"..\..\..\images\5.png", ImreadModes.GrayScale);
                        var img2 = new Mat(@"..\..\..\images\6.png", ImreadModes.GrayScale);

                        Mat dst1 = SurfFunc(img1, img2);
                        pictureBox1.Image = dst1.ToBitmap();
                        break;
                    }
                case 4:
                    {
                        var img1 = new Mat(@"..\..\..\images\7.png", ImreadModes.GrayScale);
                        var img2 = new Mat(@"..\..\..\images\8.png", ImreadModes.GrayScale);

                        Mat dst1 = SurfFunc(img1, img2);
                        pictureBox1.Image = dst1.ToBitmap();
                        break;
                    }
                case 5:
                    {
                        var img1 = new Mat(@"..\..\..\images\9.png", ImreadModes.GrayScale);
                        var img2 = new Mat(@"..\..\..\images\10.png", ImreadModes.GrayScale);

                        Mat dst1 = SurfFunc(img1, img2);
                        pictureBox1.Image = dst1.ToBitmap();
                        break;
                    }
                case 6:
                    {
                        var img1 = new Mat(@"..\..\..\images\11.png", ImreadModes.GrayScale);
                        var img2 = new Mat(@"..\..\..\images\12.png", ImreadModes.GrayScale);

                        Mat dst1 = SurfFunc(img1, img2);
                        pictureBox1.Image = dst1.ToBitmap();
                        break;
                    }
                case 7:
                    {
                        var img1 = new Mat(@"..\..\..\images\13.png", ImreadModes.GrayScale);
                        var img2 = new Mat(@"..\..\..\images\14.png", ImreadModes.GrayScale);

                        Mat dst1 = SurfFunc(img1, img2);
                        pictureBox1.Image = dst1.ToBitmap();
                        break;
                    }
                case 8:
                    {
                        var img1 = new Mat(@"..\..\..\images\15.png", ImreadModes.GrayScale);
                        var img2 = new Mat(@"..\..\..\images\16.png", ImreadModes.GrayScale);

                        Mat dst1 = SurfFunc(img1, img2);
                        pictureBox1.Image = dst1.ToBitmap();
                        break;
                    }
                case 9:
                    {
                        var img1 = new Mat(@"..\..\..\images\17.png", ImreadModes.GrayScale);
                        var img2 = new Mat(@"..\..\..\images\18.png", ImreadModes.GrayScale);

                        Mat dst1 = SurfFunc(img1, img2);
                        pictureBox1.Image = dst1.ToBitmap();
                        break;
                    }
                case 10:
                    {
                        var img1 = new Mat(@"..\..\..\images\19.png", ImreadModes.GrayScale);
                        var img2 = new Mat(@"..\..\..\images\20.png", ImreadModes.GrayScale);

                        Mat dst1 = SurfFunc(img1, img2);
                        pictureBox1.Image = dst1.ToBitmap();
                        break;
                    }
                case 11:
                    {
                        var img1 = new Mat(@"..\..\..\images\21.png", ImreadModes.GrayScale);
                        var img2 = new Mat(@"..\..\..\images\22.png", ImreadModes.GrayScale);

                        Mat dst1 = SurfFunc(img1, img2);
                        pictureBox1.Image = dst1.ToBitmap();
                        break;
                    }
                case 12:
                    {
                        var img1 = new Mat(@"..\..\..\images\23.png", ImreadModes.GrayScale);
                        var img2 = new Mat(@"..\..\..\images\24.png", ImreadModes.GrayScale);

                        Mat dst1 = SurfFunc(img1, img2);
                        pictureBox1.Image = dst1.ToBitmap();
                        break;
                    }
                case 13:
                    {
                        var img1 = new Mat(@"..\..\..\images\25.png", ImreadModes.GrayScale);
                        var img2 = new Mat(@"..\..\..\images\26.png", ImreadModes.GrayScale);

                        Mat dst1 = SurfFunc(img1, img2);
                        pictureBox1.Image = dst1.ToBitmap();
                        break;
                    }
                case 14:
                    {
                        var img1 = new Mat(@"..\..\..\images\27.png", ImreadModes.GrayScale);
                        var img2 = new Mat(@"..\..\..\images\28.png", ImreadModes.GrayScale);

                        Mat dst1 = SurfFunc(img1, img2);
                        pictureBox1.Image = dst1.ToBitmap();
                        break;
                    }
                case 15:
                    {
                        var img1 = new Mat(@"..\..\..\images\29.png", ImreadModes.GrayScale);
                        var img2 = new Mat(@"..\..\..\images\30.png", ImreadModes.GrayScale);

                        Mat dst1 = SurfFunc(img1, img2);
                        pictureBox1.Image = dst1.ToBitmap();
                        break;
                    }
            
            }
        }
    }

}
