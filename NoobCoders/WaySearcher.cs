using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobCoders
{
    internal class WaySearcher
    {
        private string[,] _map;
        private Point[,] RoadMap { get; set; }
        private Point Start { get; set; }
        private Point Finish { get; set; }
        private List<Point> RoadTrack { get; set; }

        private int MapHeight { get; set; }
        private int MapWidth { get; set; }

        public WaySearcher(string[,] map)
        {
            _map = map;
        }

        private void ReadMap()
        {
            MapHeight = _map.GetLength(0);
            MapWidth = _map.GetLength(1);
            CreateRoadMap();
            for (int column = 0; column < MapHeight; column++)
            {
                for (int row = 0; row < MapWidth; row++)
                {
                    if (_map[column, row] == ((char)MapKeysEnum.Start).ToString())
                    {
                        Start = new Point(column, row, true, (int)RoadMapKeysEnum.Start);
                        RoadMap[column, row] = Start;
                    }
                    if (_map[column,row] == ((char)MapKeysEnum.Finish).ToString())
                    {
                        Finish = new Point(column, row, false, (int)RoadMapKeysEnum.Finish);
                        RoadMap[column, row] = Finish;
                    }
                    if (_map[column,row] == ((char)MapKeysEnum.ObstacleHorizontal).ToString())
                    {
                        RoadMap[column, row] = new Point(column, row, true, (int)RoadMapKeysEnum.Obstacle);
                    }
                    if (_map[column, row] == ((char)MapKeysEnum.ObstacleVertical).ToString())
                    {
                        RoadMap[column, row] = new Point(column, row, true, (int)RoadMapKeysEnum.Obstacle);
                    }
                }
            }
        }

        private void CreateRoadMap()
        {
            RoadMap = new Point[MapHeight, MapWidth];
        }

        private void FillRoadMap()
        {
            int currentMaxValue = Start.Value;
            CheckNeighbors(Start);
            while (!Finish.isChecked)
            {
                for(int column = 0; column < MapHeight; column++)
                {
                    for (int row = 0; row < MapWidth; row++)
                    {
                        if (CheckPointExsist(column, row) && RoadMap[column, row].Value == currentMaxValue)
                        {
                            CheckNeighbors(RoadMap[column, row]);
                        }
                    }
                }
                currentMaxValue++;
            }
            Finish.Value = currentMaxValue;
        }
        private void BackTrackRoad()
        {
            int currentMaxValue = Finish.Value;
            var currentPoint = Finish;
            var trackList = new List<Point>();
            while (currentMaxValue > Start.Value)
            {
                for (int column = 0; column < MapHeight; column++)
                {
                    for (int row = 0; row < MapWidth; row++)
                    {
                        if (RoadMap[column, row] == null)
                            continue;
                        if (RoadMap[column, row].Value == currentMaxValue && Point.CheckPointIsNeighbor(currentPoint, RoadMap[column, row]))
                        {
                            trackList.Add(currentPoint);
                            currentPoint = RoadMap[column, row];
                            currentMaxValue--;
                        }
                    }
                }
            }
            trackList.Reverse();
            RoadTrack = trackList;
        }
        
        private void CheckNeighbors(Point point)
        {
            CheckPoint(point, point.Column + 1, point.Row );
            CheckPoint(point, point.Column + 1, point.Row + 1);
            CheckPoint(point, point.Column , point.Row + 1);
            CheckPoint(point, point.Column - 1, point.Row + 1);
            CheckPoint(point, point.Column - 1, point.Row );
            CheckPoint(point, point.Column - 1, point.Row - 1);
            CheckPoint(point, point.Column , point.Row - 1);
            CheckPoint(point, point.Column + 1, point.Row - 1);
        }
        private void CheckPoint(Point point, int columnToCheck, int rowToCheck)
        {
            if (CheckPointInBouns(columnToCheck, rowToCheck))
            {
                if (CheckPointExsist(columnToCheck, rowToCheck) && RoadMap[columnToCheck, rowToCheck].Value > (int)RoadMapKeysEnum.Obstacle)
                {
                    RoadMap[columnToCheck, rowToCheck].isChecked = true;
                }
                if (!CheckPointExsist(columnToCheck, rowToCheck))
                    RoadMap[columnToCheck, rowToCheck] = new Point(columnToCheck, rowToCheck, true, point.Value + 1);
            }
        }

        private bool CheckPointInBouns(int column, int row)
        {
            if (column >= MapHeight || column < 0)
                return false;
            if (row >= MapWidth || row < 0)
                return false;
            return true;  
        }

        private bool CheckPointExsist(int column, int row)
        {
            if (RoadMap[column, row] == null)
                return false;
            return true;
        }
        public void SearchWay()
        {
            ReadMap();
            FillRoadMap();
            BackTrackRoad();
            ShowTrackAnimationOnMap();
        }

        private void ShowTrackAnimationOnMap()
        {
            foreach (var point in RoadTrack)
            {
                Console.Clear();
                if (point.Value != Finish.Value)
                    _map[point.Column, point.Row] = ((char)RoadMapKeysEnum.Track).ToString();
                for (int column = 0; column < MapHeight; column++)
                {
                    Console.WriteLine();
                    for (int row = 0; row < MapWidth; row++)
                    {
                        Console.Write(_map[column, row]);

                    }
                }
                Thread.Sleep(10);
                
            }
        }
    }
}
