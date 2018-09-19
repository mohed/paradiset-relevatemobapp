using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Joyces.Droid.Views
{
    public class PunchCardProgressView : View
    {
        private int radius = 25;
        private int margin = 5;
        private Context mContext;
        public int numberOfPunchesTotal { get; set; } = 5;
        public int currentPunch { get; set; } = 3;

        public PunchCardProgressView(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize(context);
        }

        public PunchCardProgressView(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        {
            Initialize(context);
        }

        private void Initialize(Context context)
        {
            mContext = context;
        }

        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            int spacing = 2 * radius + margin;
            int shift = radius;

            var paintCircle = new Paint() { Color = Color.ParseColor(GeneralSettings.PunchCardWidgetColor) };
            drawCircles(spacing, shift, canvas, paintCircle, 0, currentPunch);

            paintCircle.SetStyle(Paint.Style.Stroke);
            drawCircles(spacing, shift, canvas, paintCircle, currentPunch, numberOfPunchesTotal);
           
        }

        private void drawCircles(int spacing, int shift, Canvas canvas, Paint paintCircle, int start, int end)
        {   
            for (int i = start; i < end; i++)
            {
                int x = i * spacing + shift;
                int y = radius;
                canvas.DrawCircle(x, y, radius, paintCircle);
            }
        }
    }
}