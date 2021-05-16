using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdditonalMaths
{
    public static class AdditonalMaths
    {
        public static int UnsignedModulo(this int left, int right)
        {
            return (left + right) % right;
        }
    }
}