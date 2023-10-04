namespace Notes.ViewModels;

public record ModelWithUserId<T>(string UserId, T Model);