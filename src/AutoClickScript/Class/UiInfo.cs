using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AutoClickScript
{
    public class UiInfo
    {
        public ImageInfo _img;
        public Point _clickLoca;

        public UiInfo()
        { }

        public UiInfo(Bitmap img, Point img_loca, Point click_loca)
        {
            _img = new ImageInfo();
            _img._img = img;
            _img._screenLoca1 = img_loca;
            _img._relativeLocal = Common.GetRelativeLoca(img_loca, MainForm.processInfo._Location);
            _clickLoca = Common.GetRelativeLoca(click_loca, MainForm.processInfo._Location);
        }
    }
}
