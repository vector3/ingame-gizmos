﻿using UnityEngine;

namespace Popcron
{
    public class Polygon : Drawer
    {
        public override Vector3[] Draw(DrawInfo drawInfo)
        {
            Vector3 position = drawInfo.vectors[0];
            int points = (int)(drawInfo.floats.Count > 0 ? drawInfo.floats[0] : 16f);
            float radius = drawInfo.floats.Count > 1 ? drawInfo.floats[1] : 16f;
            float offset = drawInfo.floats.Count > 2 ? drawInfo.floats[2] : 0f;
            Quaternion rotation = drawInfo.rotation ?? Camera.main.transform.rotation;

            return Draw(position, rotation, radius, points, offset);
        }

        internal static Vector3[] Draw(Vector3 position, Quaternion rotation, float radius, int points, float offset)
        {
            float angle = 360f / points;
            offset *= Mathf.Deg2Rad;

            Vector3[] lines = new Vector3[2 * points];
            for (int i = 0; i < points; i++)
            {
                float sx = Mathf.Cos(Mathf.Deg2Rad * angle * i + offset) * radius * 0.5f;
                float sy = Mathf.Sin(Mathf.Deg2Rad * angle * i + offset) * radius * 0.5f;

                float nx = Mathf.Cos(Mathf.Deg2Rad * angle * (i + 1) + offset) * radius * 0.5f;
                float ny = Mathf.Sin(Mathf.Deg2Rad * angle * (i + 1) + offset) * radius * 0.5f;

                Vector3 a = new Vector3(sx, sy);
                Vector3 b = new Vector3(nx, ny);

                a = rotation * a;
                b = rotation * b;

                lines[i * 2] = position + a;
                lines[i * 2 + 1] = position + b;
            }

            return lines;
        }
    }
}
