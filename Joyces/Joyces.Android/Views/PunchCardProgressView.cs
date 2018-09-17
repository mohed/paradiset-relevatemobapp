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
        private int radius = 20;
        private int margin = 5;
        private Context mContext;
        private int NUM_CIRCLES = 5;

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

            Paint debugLayout = new Paint();
            debugLayout.Color = Color.LightGray;
            canvas.DrawPaint(debugLayout);

            

            
            paintPunchedSlots(spacing, shift, canvas, 3);
            paintUnPuncedSlot(spacing, 3*shift, canvas, 2);
            // x and y needs to be obtaind from parrent view dynamicaly

            /*
            Paint paint = new Paint();
            paint.SetStyle(Paint.Style.FillAndStroke);
            paint.Color = Color.White;
            canvas.DrawPaint(paint);
            paint.Color = Color.ParseColor("#da4747");
            canvas.DrawCircle(x, y, radius, paint);
            */
        }

        private void paintPunchedSlots(int spacing, int shift, Canvas canvas, int num)
        {
            var paintCircle = new Paint() { Color = Color.ParseColor(GeneralSettings.PunchCardWidgetColor) };
            for (int i = 0; i < num; i++)
            {
                int x = i * spacing + shift;
                int y = radius;
                canvas.DrawCircle(x, y, radius, paintCircle);
            }
        }

        private void paintUnPuncedSlot(int spacing, int shift, Canvas canvas, int num)
        {
            var paintCircle = new Paint()
            { Color = Color.ParseColor(GeneralSettings.PunchCardWidgetColor) };
            paintCircle.SetStyle(Paint.Style.Stroke);
            for (int i = 0; i < NUM_CIRCLES; i++)
            {
                int x = i * spacing + shift;
                int y = radius;
                canvas.DrawCircle(x, y, radius, paintCircle);
            }
        }
    }
}