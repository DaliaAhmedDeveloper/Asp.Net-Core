LogTrace("msg") → المستوى Trace

LogDebug("msg") → المستوى Debug

LogInformation("msg") → المستوى Information

LogWarning("msg") → المستوى Warning

LogError("msg") → المستوى Error

LogCritical("msg") → المستوى Critical


using (_logger.BeginScope("OrderId: {OrderId}", 123))
{
    _logger.LogInformation("Processing order started");
    _logger.LogError("Order failed");
}


using (_logger.BeginScope("OrderId: {OrderId}", 123))
{
    _logger.LogInformation("Processing order started");
    _logger.LogError("Order failed");
}
تقصد هنا بدل ما يطبع error : 
بيطبع حاجه تانيه حسب ماحط وكانى بعمل جروب على شويه logs تبتدى بكلمه معينه ؟
ChatGPT said:

بالضبط ✅ ده بالضبط اللي قصديته.

1️⃣ الفكرة العامة

الـ Scope بيضيف Context ثابت أو بيانات إضافية لكل Log جوه الـ Scope ده.

يعني كل Log جوه الـ using هيظهر مع البيانات دي.

2️⃣ في المثال بتاعك
using (_logger.BeginScope("OrderId: {OrderId}", 123))
{
    _logger.LogInformation("Processing order started");
    _logger.LogError("Order failed");
}


هنا كل Log هيكون مربوط بالـ OrderId = 123.

النتيجة في الـ Console ممكن تبقى كده:

info: OnlineStore.OrderService[0]
      => OrderId: 123 => Processing order started
fail: OnlineStore.OrderService[0]
      => OrderId: 123 => Order failed


✅ يعني كل Log جوه الـ Scope مُعرف بـ OrderId، وكأنك عملت group للـ Logs المتعلقة بنفس العملية.

3️⃣ فايدة ده عمليًا

لو عندك System فيه آلاف الـ Logs يوميًا، ممكن تعرف بسهولة أي Logs تخص أي Request أو Transaction.

مثال: كل طلب شراء (Order) ممكن تعمل Scope على OrderId، فتقدر تتابع كل الأحداث المرتبطة بالطلب ده.

4️⃣ ملاحظات

Scope مش بيغير الرسالة نفسها، بس يضيف context قبل أو بعد الرسالة.

لو مش محتاج context، ممكن ترجّع null! في BeginScope() زي ما عملت في الـ DbLogger.

طيب وانا كده لكل exception مرتبط بحاجه معينه لازم اعمله custom ilogger class ?
ChatGPT said:

لا مش شرط تعمل Custom ILogger لكل Exception أو لكل عملية، لكن فيه شوية حاجات مهمة لازم تعرفيها:

1️⃣ الحالة العادية

لو انت راضية إن كل Exception يتسجل بنفس الطريقة (StackTrace + InnerException + Title/Message)، كفاية Logger واحد زي اللي عملتي (DbLogger).

الـ DbLogger ممكن يستقبل state أو context (زي LogState أو Scope) لكل استثناء، وبالتالي كل Exception مرتبط بحاجة معينة ممكن يتسجل بنفس Logger.

2️⃣ إزاي تربطي Exception بحاجة معينة
أ) باستخدام Scope
using (_logger.BeginScope("OrderId: {OrderId}", orderId))
{
    try
    {
        // عملية ممكن تفشل
    }
    catch (Exception ex)
    {
        _logger.LogErrorWithTranslations(
            ex,
            "Database Error", "Failed to connect to the database",
            "خطأ في قاعدة البيانات", "فشل الاتصال بقاعدة البيانات"
        );
    }
}


كل Exception داخل الـ Scope هيكون مرتبط بالـ OrderId.

مش محتاج Logger مختلف لكل OrderId، نفس Logger بيتعامل مع كل الحاجات.

ب) باستخدام state / LogState

لما تبعتي LogState (Title/Message/Translations)، ممكن كل Exception يكون عنده تفاصيل مختلفة.

الـ DbLogger يقرأ الـ state ويحفظ كل شيء بالـ Database.

3️⃣ خلاصة

لازم Custom Logger مرة واحدة فقط لو عايزة تخزن Logs في DB بالصيغة الخاصة (StackTrace + InnerException + Title/Message + Translations).

الارتباط بأي شيء محدد (Order, User, API...) ممكن يتعمل بـ:

Scope → context ثابت لكل مجموعة Logs

State object (LogState) → بيانات ديناميكية لكل Log