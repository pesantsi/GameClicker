namespace GameClicker.Services
{
    public interface IFileManager
    {
        bool Save<T>(T objectToSave);

        bool Load<T>(out T objectLoaded);
    }
}