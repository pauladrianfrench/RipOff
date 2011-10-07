using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RipOff
{
    public class GameArea
    {
        List<IEntity> gameObjects;

        public DrawParams DrawParam { get; set; }

        public GameArea()
        {
            gameObjects = new List<IEntity>();
        }

        public void Draw()
        {
            int count = gameObjects.Count;
            for (int i = 0; i < count; ++i)
            {
                gameObjects[i].Draw(this.DrawParam);
            }
        }

        public void AddGameObject(IEntity obj)
        {
            bool playerExists = false;
            int count = gameObjects.Count;
            for (int i = 0; i < count; ++i)
            {
                if (gameObjects[i] is PlayerVehicle)
                {
                    playerExists = true;
                    if (obj is EnemyTank)
                    {
                        (obj as EnemyTank).Target = gameObjects[i];
                    }
                }
            }
            if (!(obj is PlayerVehicle))
            {
                gameObjects.Add(obj);
            }
            else if (!playerExists)
            {
                gameObjects.Add(obj);
            }
        }
      
        public void Update()
        {
            int count = gameObjects.Count;
            for (int i = count-1; i >= 0; --i)
            {
                if (gameObjects[i].Expired)
                {
                    gameObjects.RemoveAt(i);
                }
                else
                {
                    gameObjects[i].Update();
                }
            }
            CheckCollisions();
        }

        public void CheckCollisions()
        {
            int count = gameObjects.Count;
            for (int i = 0; i < count; ++i)
            {
                if (gameObjects[i] is IMovingEntity)
                {
                    for (int j = 0; j < count; ++j)
                    {
                        if (i != j)
                        {
                            (gameObjects[i] as IMovingEntity).DetectProximity(gameObjects[j]);
                        }
                    }
                }
            }
        }

        public void KeyDown(ActionParams actions)
        {
            int count = gameObjects.Count;
            for (int i = 0; i < count; ++i)
            {
                if (gameObjects[i] is PlayerVehicle)
                {
                    (gameObjects[i] as PlayerVehicle).KeyDown(actions);
                }
            }
        }

        public void KeyUp(ActionParams actions)
        {
            int count = gameObjects.Count;
            for (int i = 0; i < count; ++i)
            {
                if (gameObjects[i] is PlayerVehicle)
                {
                    (gameObjects[i] as PlayerVehicle).KeyUp(actions);
                }
            }
        }
    }
}
