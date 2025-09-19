using System.Resources;

namespace OnlineStore.Resources;

public class ValidationMessages
{
        private static ResourceManager resourceMan = new ResourceManager("OnlineStore.Resources.ValidationMessages", typeof(ValidationMessages).Assembly);
        public static string FullNameRequired => resourceMan.GetString("FullNameRequired") ?? "Full Name is required.";
        public static string EmailRequired => resourceMan.GetString("EmailRequired") ?? "Email is required.";
        public static string EmailInvalid => resourceMan.GetString("EmailInvalid") ?? "Please enter a valid email address.";
        public static string EmailUnique => resourceMan.GetString("EmailUnique") ?? "Email must be unique.";
        public static string PhoneRequired => resourceMan.GetString("PhoneRequired") ?? "Phone number is required.";
        public static string PhoneInvalid => resourceMan.GetString("PhoneInvalid") ?? "Invalid phone number format.";
        public static string PhoneUnique => resourceMan.GetString("PhoneUnique") ?? "Phone number must be unique.";
        public static string PasswordRequired => resourceMan.GetString("PasswordRequired") ?? "Password is required.";
        public static string PasswordComplexity => resourceMan.GetString("PasswordComplexity") ?? "Password must be at least 8 characters, include uppercase, lowercase, digit, and special char.";
        public static string PasswordsDoNotMatch => resourceMan.GetString("PasswordsDoNotMatch") ?? "Password and confirmation do not match.";

        public static string ProductIdRequired => resourceMan.GetString("ProductIdRequired") ?? "Product Id is required.";
        public static string VariantIdRequired => resourceMan.GetString("VariantIdRequired") ?? "Variant Id is required.";
        public static string CartIdRequired => resourceMan.GetString("CartIdRequired") ?? "Cart Id is required.";
        public static string QuantityRequired => resourceMan.GetString("QuantityRequired") ?? "Quantity is required.";
        public static string CountryRequired => resourceMan.GetString("CountryRequired") ?? "Country is required.";
        public static string CityRequired => resourceMan.GetString("CityRequired") ?? "City is required.";
        public static string StreetRequired => resourceMan.GetString("StreetRequired") ?? "Street is required.";
        public static string ZipCodeRequired => resourceMan.GetString("ZipCodeRequired") ?? "Zip Code is required.";
        public static string UserIdRequired => resourceMan.GetString("UserIdRequired") ?? "User Id is required.";
        public static string OrderIdRequired => resourceMan.GetString("OrderIdRequired") ?? "Order Id is required.";
        public static string CategoryRequired => resourceMan.GetString("CategoryRequired") ?? "Category is required.";
        public static string SubjectRequired => resourceMan.GetString("SubjectRequired") ?? "Subject is required.";
        public static string SubjectLimit => resourceMan.GetString("SubjectLimit") ?? "Subject cannot exceed 200 characters.";
        public static string PaymentRequired => resourceMan.GetString("PaymentRequired") ?? "Payment Method is required.";
        public static string DescriptionRequired => resourceMan.GetString("DescriptionRequired") ?? "Description is required.";

        public static string TicketIdRequired => resourceMan.GetString("TicketIdRequired") ?? "Ticket Id is required.";
        public static string TicketMessageUserIdRequired => resourceMan.GetString("TicketMessageUserIdRequired") ?? "User Id is required.";
        public static string TicketMessageRequired => resourceMan.GetString("TicketMessageRequired") ?? "Message is required.";
        public static string TicketMessageLimit => resourceMan.GetString("TicketMessageLimit") ?? "Message cannot exceed 1000 characters.";

        public static string LoginEmailRequired => resourceMan.GetString("LoginEmailRequired") ?? "Email is required.";
        public static string LoginEmailInvalid => resourceMan.GetString("LoginEmailInvalid") ?? "Invalid Email.";
        public static string LoginPasswordRequired => resourceMan.GetString("LoginPasswordRequired") ?? "Password is required.";
        public static string ResetEmailRequired => resourceMan.GetString("ResetEmailRequired") ?? "Email is required.";
        public static string ResetEmailInvalid => resourceMan.GetString("ResetEmailInvalid") ?? "Invalid email format.";
        public static string ResetNewPasswordRequired => resourceMan.GetString("ResetNewPasswordRequired") ?? "New password is required.";
        public static string ResetTokenRequired => resourceMan.GetString("ResetTokenRequired") ?? "Reset token is required.";
        public static string ResetNewPasswordWeak => resourceMan.GetString("ResetNewPasswordWeak") ?? "Password must be at least 8 characters long.";
        public static string CartItemIdRequired => resourceMan.GetString("CartItemIdRequired") ?? "Cart Item Id Is Required.";
        public static string AddressIdRequired => resourceMan.GetString("AddressIdRequired") ?? "Address Id Is Required.";
        public static string OrderItemIdRequired => resourceMan.GetString("OrderItemIdRequired") ?? "Order Item Id is required.";
        public static string CommentRequired => resourceMan.GetString("CommentRequired") ?? "Comment is required.";
        public static string RatingRequired => resourceMan.GetString("RatingRequired") ?? "Rating is required.";
        public static string ReasonRequired => resourceMan.GetString("ReasonRequired") ?? "Reason is required.";
        public static string ReasonMinLength => resourceMan.GetString("ReasonMinLength") ?? "Reason must be at least 30 characters long.";
        public static string PriceFromRange => resourceMan.GetString("PriceFromRange") ?? "PriceFrom must be greater than or equal to 0.";
        public static string PriceToRange => resourceMan.GetString("PriceToRange") ?? "PriceTo must be greater than or equal to 0.";
        public static string PriceToGreaterThanPriceFrom => resourceMan.GetString("PriceToGreaterThanPriceFrom") ?? "PriceTo must be greater than or equal to PriceFrom.";

}
