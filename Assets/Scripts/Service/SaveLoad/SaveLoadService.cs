using System;
using System.IO;
using System.Linq;
using MemoryPack;
using Model;
using Repository;
using UnityEngine;
using VContainer.Unity;

namespace Service.SaveLoad
{
    public class SaveLoadService : IStartable, IPostFixedTickable, IDisposable
    {
        private readonly string filePath;
        private readonly BallFactory factory;
        private readonly BallModel model;
        private readonly BallsRepository repository;

        private BallSerializedData[] ballsData;
        private bool isNeedSave = true;

        public SaveLoadService(BallFactory factory, BallsRepository repository, BallModel model)
        {
            this.factory = factory;
            this.repository = repository;
            this.model = model;
            filePath = Application.persistentDataPath + "/save.bin";
        }

        public void ClearSave()
        {
            isNeedSave = false;
            File.Delete(filePath);
        }

        public void Start()
        {
            LoadBalls();
        }

        public void PostFixedTick()
        {
            if (!isNeedSave)
            {
                return;
            }
            
            ballsData = repository.Values
                .Where(b => b.Key.IsSimulated)
                .Select(b => new BallSerializedData
                {
                    Position = b.Key.Position, 
                    Value = b.Value
                }).ToArray();
                
            SaveBalls();
        }

        public void Dispose()
        {
            if (isNeedSave)
            {
                SaveBalls();
            }
        }

        private void SaveBalls()
        {
            var bytes = MemoryPackSerializer.Serialize(ballsData);
            File.WriteAllBytes(filePath, bytes);
        }

        private void LoadBalls()
        {
            if (!File.Exists(filePath))
            {
                return;
            }
            
            var bytes = File.ReadAllBytes(filePath);
            var data = MemoryPackSerializer.Deserialize<BallSerializedData[]>(bytes);

            foreach (var d in data)
            {
                var ball = factory.CreateBall(d.Value, d.Position);
                model.EnablePhysic(ball, Vector2.zero);
            }
        }
    }
}