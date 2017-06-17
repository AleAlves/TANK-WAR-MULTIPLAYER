using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tank_War_Multiplayer
{
    public class ParticleEngine : Game
    {
        private Random random;
        public Vector2 EmitterLocation { get; set; }
        private List<Particle> particles;
        private List<Texture2D> textures;

        public ParticleEngine(List<Texture2D> textures, Vector2 location)
        {
            EmitterLocation = location;
            this.textures = textures;
            this.particles = new List<Particle>();
            random = new Random();
        }

        public void Update(int x, int y, int anguloTiro)
        {
            int total = 15;

            for (int i = 0; i < total; i++)
            {
                switch (anguloTiro)
                {
                    case 1:
                    case 49:
                        particles.Add(GenerateNewParticle(x, y--, i));
                        break;
                    case 2:
                    case 50:
                        particles.Add(GenerateNewParticle(x, y++, i));
                        break;
                    case 3:
                    case 51:
                        particles.Add(GenerateNewParticle(x++, y, i));
                        break;
                    case 4:
                    case 52:
                        particles.Add(GenerateNewParticle(x--, y, i));
                        break;
                }
            }

            for (int particle = 0; particle < particles.Count; particle++)
            {
                particles[particle].Update();
                if (particles[particle].TTL <= 0)
                {
                    particles.RemoveAt(particle);
                    particle--;
                }
            }
        }
        static float size = 0.3f;
        private Particle GenerateNewParticle(int x, int y, int i)
        {
            Texture2D texture = textures[0];
            Vector2 position = new Vector2(x, y);
            Vector2 velocity = new Vector2(0, 0);
            Vector2 center = new Vector2(0,0);
            float angle = 0f;
            float angularVelocity = 0.0f;
            size = 0.15f;
            int ttl = 5;
            Color color = new Color(Color.Orange,1);

            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);
        }


        public void Clear()
        {
            this.particles.Clear();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            try
            {
                for (int index = 0; index < particles.Count; index++)
                {
                    particles[index].Draw(spriteBatch);
                }
            }
            catch { }
        }
    }
}