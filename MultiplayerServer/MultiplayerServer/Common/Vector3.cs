using System;

namespace MultiplayerServer.Common
{
    public class Vector3
    {
        private readonly Tuple<float, float, float> _vectors;

        public float X => _vectors.Item1; 
        public float Y => _vectors.Item2;
        public float Z => _vectors.Item3;

        public Vector3(int x, int y, int z)
        {
            _vectors = new Tuple<float, float, float>(x, y, z);
        }
    }
}
