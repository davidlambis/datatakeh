﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATATAKEH.Interfaces
{
    public interface IImageResize
    {
        byte[] ResizeImage(byte[] imageData, float width, float height);
    }
}
