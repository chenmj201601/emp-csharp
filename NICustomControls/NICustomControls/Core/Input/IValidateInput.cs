//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    3d058a3e-03f2-40d8-b24c-0de59a75d144
//        CLR Version:              4.0.30319.42000
//        Name:                     IValidateInput
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Wpf.Controls.Core.Input
//        File Name:                IValidateInput
//
//        Created by Charley at 2017/4/28 15:59:01
//        http://www.netinfo.com 
//
//======================================================================

namespace NetInfo.Wpf.Controls.Core.Input
{
    public interface IValidateInput
    {
        event InputValidationErrorEventHandler InputValidationError;
        bool CommitInput();
    }
}
