using Models.Enums;

namespace Models.Shared
{
    public interface ISwitchable
    {// thuộc  tính ,đối tượng đã được kích hoạt hay ko???
        Status Status { set; get; }
    }
}