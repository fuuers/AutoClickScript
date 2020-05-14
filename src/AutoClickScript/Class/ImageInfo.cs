using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AutoClickScript
{
    public class ImageInfo
    {
        public Bitmap _img;

        /// <summary>
        /// 图片屏幕坐标
        /// </summary>
        public Point _screenLoca1;

        /// <summary>
        /// 相对坐标
        /// </summary>
        public Point _relativeLocal;

        /// <summary>
        /// 
        /// </summary>
        private string _id = string.Empty;
        /// <summary>
        /// 图片特征码，根据感知哈希算法计算出来
        /// </summary>
        public string ID
        {
            get 
            {
                if (_id == string.Empty)
                {
                    if (_img != null)
                    {
                        ImageDiff diff = new ImageDiff(_img);
                        _id = diff.GetHash();
                    }
                }
                return _id;
            }
        }
    }
}
