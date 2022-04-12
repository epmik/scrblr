﻿using OpenTK.Mathematics;
using System;
using System.Linq;
using System.Reflection;

namespace Scrblr.Core
{
    public static class MatrixExtensions
    {
        public static Matrix4 Copy(this Matrix4 matrix)
        {
            return new Matrix4(matrix.M11, matrix.M12, matrix.M13, matrix.M14,
                matrix.M21, matrix.M22, matrix.M23, matrix.M14,
                matrix.M31, matrix.M32, matrix.M33, matrix.M34,
                matrix.M41, matrix.M42, matrix.M43, matrix.M44);
        }
    }
}