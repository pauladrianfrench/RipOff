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

        public GameArea(DrawParams dp)
        {
            this.DrawParam = dp;
            gameObjects = new List<IScreenEntity>();

            // by convention we'll make the player's tank the first item.
            gameObjects.Add(new PlayerVehicle(this));
        }

        public void Draw()
        {
            int count = gameObjects.Count;
            for (int i = 0; i < count; ++i)
            {
                gameObjects[i].Draw(this.DrawParam);
            }
        }
      
        public void Update()
        {
            int count = gameObjects.Count;
            for (int i = 0; i < count; ++i)
            {
                gameObjects[i].Update();
            }
        }

        public void KeyDown(ActionParams actions)
        {
            (gameObjects[0] as PlayerVehicle).KeyDown(actions);
        }

        public void KeyUp(ActionParams actions)
        {
            (gameObjects[0] as PlayerVehicle).KeyUp(actions);
        }
    }
}
