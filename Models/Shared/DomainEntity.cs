namespace Models.Shared
{
    public abstract class DomainEntity<T>
    {
        public T Id { get; set; }

        // check xem id có bằng với giá trị mặc định của T hay ko, true nếu domain entity được xét tự động tăng r
        public bool IsTransient()
        {
            return Id.Equals(default(T));
        }
    }
}