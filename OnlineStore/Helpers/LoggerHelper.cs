namespace OnlineStore.Helpers;
public static class LoggerHelper
{
    public static void LogErrorWithTranslations(
        this ILogger logger, // add extension method to Ilogger interface 
        Exception ex, // ex of type exception
        string titleEn, string messageEn,
        string titleAr, string messageAr)
    {
        logger.LogError(ex, $"{titleEn}: {messageEn}"); // log based on logger configuration ,, console , file , db

        // send state to add to custom db table including ar and en title and message
        logger.Log(
            LogLevel.Error, // type ,, loglevel is enum
            new EventId(0, "CustomError"), // ??
            new LogState(titleEn, messageEn, titleAr, messageAr), // state ,, to send additional info
            ex, // exception
            (state, e) => $"{state.TitleEn}: {state.MessageEn}" // message 
        );
    }
    // helper record to carry translations
    // record its like a class but in shorter way ,, its automatically assign values sent to properties 
    public record LogState(string TitleEn, string MessageEn, string TitleAr, string MessageAr);
}
