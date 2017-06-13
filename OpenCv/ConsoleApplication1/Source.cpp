#include <iostream>
#include <string>
#include <vector>

#include <opencv2/core/core.hpp>
#include <opencv2/highgui/highgui.hpp>
#include <opencv2/features2d/features2d.hpp>
#include <opencv2/nonfree/features2d.hpp>
#include <opencv2/legacy/legacy.hpp>

using namespace cv;

int main(int argc, char** argv)
{
	if (argc != 3) return 1;

	Mat ImageTemple = imread(argv[1], CV_LOAD_IMAGE_GRAYSCALE);
	if (!ImageTemple.data)
		return 2; // Ошибка

	Mat Image = imread(argv[2], CV_LOAD_IMAGE_GRAYSCALE);
	if (!Image.data)
		return 3; // Ошибка

	std::vector<KeyPoint> keypointsImageTemple, keypointsImage;
	Mat descriptorsImageTemple, descriptorsImage;
	std::vector<DMatch> matches;
	// Инициалищация класса детектора особенностей, 1000 - пороговое значение для отсеивания 
	// малозначимых особенностей
	SurfFeatureDetector detector(1000);

	// Класс для FREAK особенностей. Можно настраивать режимы сравнения особенностей:
	// FREAK extractor(true, true, 22, 4, std::vector<int>());
	FREAK extractor;

	// Используется для определение совпадений особенностей - мера Хемминга
	BruteForceMatcher<Hamming> matcher;

	// Детектирование
	double t = (double)getTickCount();

	detector.detect(ImageTemple, keypointsImageTemple);
	detector.detect(Image, keypointsImage);

	t = ((double)getTickCount() - t) / getTickFrequency();
	std::cout << "detection time [s]: " << t / 1.0 << std::endl;

	// Извлечение особенностей
	t = (double)getTickCount();

	extractor.compute(ImageTemple, keypointsImageTemple, descriptorsImageTemple);
	extractor.compute(Image, keypointsImage, descriptorsImage);

	t = ((double)getTickCount() - t) / getTickFrequency();
	std::cout << "extraction time [s]: " << t << std::endl;

	// Сравнение
	t = (double)getTickCount();

	matcher.match(descriptorsImageTemple, descriptorsImage, matches);

	t = ((double)getTickCount() - t) / getTickFrequency();
	std::cout << "matching time [s]: " << t << std::endl;

	// Отобразить на изображении
	Mat imgMatch;

	drawMatches(ImageTemple, keypointsImageTemple, Image, keypointsImage, matches, imgMatch);
	imwrite("matches.jpeg", imgMatch);

	std::vector<Point2f> obj;
	std::vector<Point2f> scene;
	for (int i = 0; i < matches.size(); i++)
	{
		obj.push_back(keypointsImageTemple[matches[i].queryIdx].pt);
		scene.push_back(keypointsImage[matches[i].trainIdx].pt);
	}       Mat H = findHomography(obj, scene, CV_RANSAC);        std::vector<Point2f> obj_corners(4);
	obj_corners[0] = cvPoint(0, 0); obj_corners[1] = cvPoint(ImageTemple.cols, 0);
	obj_corners[2] = cvPoint(ImageTemple.cols, ImageTemple.rows); obj_corners[3] = cvPoint(0, ImageTemple.rows);
	std::vector<Point2f> scene_corners(4);

	perspectiveTransform(obj_corners, scene_corners, H);

	//-- Draw lines between the corners (the mapped object in the scene - image_2 )
	line(imgMatch, scene_corners[0] + Point2f(ImageTemple.cols, 0), scene_corners[1] + Point2f(ImageTemple.cols, 0), Scalar(0, 255, 0), 4);
	line(imgMatch, scene_corners[1] + Point2f(ImageTemple.cols, 0), scene_corners[2] + Point2f(ImageTemple.cols, 0), Scalar(0, 255, 0), 4);
	line(imgMatch, scene_corners[2] + Point2f(ImageTemple.cols, 0), scene_corners[3] + Point2f(ImageTemple.cols, 0), Scalar(0, 255, 0), 4);
	line(imgMatch, scene_corners[3] + Point2f(ImageTemple.cols, 0), scene_corners[0] + Point2f(ImageTemple.cols, 0), Scalar(0, 255, 0), 4);
	imwrite("matches3.jpeg", imgMatch);
	return 0;
}