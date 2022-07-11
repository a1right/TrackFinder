using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobCoders
{
    internal class WaySearcher
    {
        private readonly string[,] _map;
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

        public void SearchWay()
        {
            ReadMap();
            FillRoadMap();
            BackTrackRoad();
            ShowTrackAnimationOnMap();
        }

        private void ReadMap()
        {
            MapHeight = _map.GetLength(0);
            MapWidth = _map.GetLength(1);
            RoadMap = new Point[MapHeight, MapWidth];

            for (int column = 0; column < MapHeight; column++)
            {
                for (int row = 0; row < MapWidth; row++)
                {
                    if (_map[column, row] == ((char)MapKeys.Start).ToString())
                    {
                        Start = new Point(column, row, true, (int)RoadMapKeys.Start);
                        RoadMap[column, row] = Start;
                    }
                    if (_map[column,row] == ((char)MapKeys.Finish).ToString())
                    {
                        Finish = new Point(column, row, false, (int)RoadMapKeys.Finish);
                        RoadMap[column, row] = Finish;
                    }
                    if (_map[column,row] == ((char)MapKeys.ObstacleHorizontal).ToString())
                    {
                        RoadMap[column, row] = new Point(column, row, true, (int)RoadMapKeys.Obstacle);
                    }
                    if (_map[column, row] == ((char)MapKeys.ObstacleVertical).ToString())
                    {
                        RoadMap[column, row] = new Point(column, row, true, (int)RoadMapKeys.Obstacle);
                    }
                }
            }
        }

        private void FillRoadMap()
        {
            int currentMaxValue = Start.Value;
            while (!Finish.IsChecked)
            {
                for(int column = 0; column < MapHeight; column++)
                {
                    for (int row = 0; row < MapWidth; row++)
                    {
                        if (IsCheckPointExists(column, row) && RoadMap[column, row].Value == currentMaxValue)
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
                        if (RoadMap[column, row].Value == currentMaxValue && Point.IsNeighborCheckPoint(currentPoint, RoadMap[column, row]))
                        {
                            trackList.Add(currentPoint);
                            currentPoint = RoadMap[column, row];
                            currentMaxValue--;
                        }
                    }
                }
            }
            trackList.RemoveAt(0);
            trackList.Reverse();
            RoadTrack = trackList;
        }
        private void ShowTrackAnimationOnMap()
        {
            foreach (var point in RoadTrack)
            {
                Console.SetCursorPosition(point.Row, point.Column);
                Console.Write((char)RoadMapKeys.Track);
                Thread.Sleep(30);
            }
        }

        private void CheckNeighbors(Point point)
        {
            CheckPoint(point.Column + 1, point.Row, point.Value);
            CheckPoint(point.Column + 1, point.Row + 1, point.Value);
            CheckPoint(point.Column , point.Row + 1, point.Value);
            CheckPoint(point.Column - 1, point.Row + 1, point.Value);
            CheckPoint(point.Column - 1, point.Row, point.Value);
            CheckPoint(point.Column - 1, point.Row - 1, point.Value);
            CheckPoint(point.Column , point.Row - 1, point.Value);
            CheckPoint(point.Column + 1, point.Row - 1, point.Value);
        }
        private void CheckPoint(int columnToCheck, int rowToCheck, int currentPointValue)
        {
            if (!IsCheckPointInBounds(columnToCheck, rowToCheck)) return;

            if (IsCheckPointExists(columnToCheck, rowToCheck) && RoadMap[columnToCheck, rowToCheck].Value > (int)RoadMapKeys.Obstacle)
                RoadMap[columnToCheck, rowToCheck].IsChecked = true;

            if (!IsCheckPointExists(columnToCheck, rowToCheck))
                RoadMap[columnToCheck, rowToCheck] = new Point(columnToCheck, rowToCheck, true, currentPointValue + 1);
        }

        private bool IsCheckPointInBounds(int column, int row)
        {
            if (column >= MapHeight || column < 0)
                return false;
            return row < MapWidth && row >= 0;
        }

        private bool IsCheckPointExists(int column, int row) => RoadMap[column, row] != null;
    }
}
