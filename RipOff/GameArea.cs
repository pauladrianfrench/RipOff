using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RipOff
{
    public class GameArea
    {
        List<IScreenEntity> gameObjects;

        public DrawParams DrawParam { get; set; }

        public GameArea()
        {
            gameObjects = new List<IScreenEntity>();
        }

        public void Draw()
        {
            int count = gameObjects.Count;
            for (int i = 0; i < count; ++i)
            {
                gameObjects[i].Draw(this.DrawParam);
            }
        }

        public void AddGameObject(IScreenEntity obj)
        {
            bool playerExists = false;
            int count = gameObjects.Count;
            for (int i = 0; i < count; ++i)
            {
                if (gameObjects[i] is PlayerVehicle)
                {
                    playerExists = true;
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
                for (int j = i+1; j < count; ++j)
                {
                    gameObjects[i].DetectCollision(gameObjects[j]);
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
