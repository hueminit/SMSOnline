using System;
using System.Collections.Generic;
using System.Text;
using Model.Enums;

namespace Model.Shared
{
    public interface ISwitchable
    {// thuộc  tính ,đối tượng đã được kích hoạt hay ko???
        Status Status { set; get; }
    }
}
