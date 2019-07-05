using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace Xamarin.Demos.Controls
{
    internal static class PermissionHelper
    {
        internal static async Task<bool> RequestCamera()
        {
            try
            {
                //Check the current status of the permission.
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync<CameraPermission>();

                //If it's already granted, just finish the verification.
                if (status == PermissionStatus.Granted)
                    return true;

                if (status == PermissionStatus.Disabled)
                {
                    return false;
                }

                if (status != PermissionStatus.Granted)
                {
                    status = await CrossPermissions.Current.RequestPermissionAsync<CameraPermission>();
                }

                return status == PermissionStatus.Granted;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        internal static async Task<bool> RequestMediaLibrary()
        {
            try
            {
                //Check the current status of the permission.
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync<MediaLibraryPermission>();

                //If it's already granted, just finish the verification.
                if (status == PermissionStatus.Granted)
                    return true;

                if (status == PermissionStatus.Disabled)
                {
                    return false;
                }

                if (status != PermissionStatus.Granted)
                {
                    status = await CrossPermissions.Current.RequestPermissionAsync<MediaLibraryPermission>();
                }

                return status == PermissionStatus.Granted;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
