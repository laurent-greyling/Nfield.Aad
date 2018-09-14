using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using static Android.App.Application;

namespace Nfield.Aad.Droid
{
    [Application]
    public partial class MainApplication : Application, IActivityLifecycleCallbacks
    {
        internal static Context CurrentContext { get; private set; }

        public MainApplication(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer) { }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            CurrentContext = activity;
        }

        public override void OnCreate()
        {
            base.OnCreate();
            RegisterActivityLifecycleCallbacks(this);
        }

        public override void OnTerminate()
        {
            base.OnTerminate();
            UnregisterActivityLifecycleCallbacks(this);
        }

        public void OnActivityDestroyed(Activity activity)
        {
            CurrentContext = activity;
        }

        public void OnActivityPaused(Activity activity)
        {
            CurrentContext = activity;
        }

        public void OnActivityResumed(Activity activity)
        {
            CurrentContext = activity;
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
            CurrentContext = activity;
        }

        public void OnActivityStarted(Activity activity)
        {
            CurrentContext = activity;
        }

        public void OnActivityStopped(Activity activity)
        {
            CurrentContext = activity;
        }
    }
}