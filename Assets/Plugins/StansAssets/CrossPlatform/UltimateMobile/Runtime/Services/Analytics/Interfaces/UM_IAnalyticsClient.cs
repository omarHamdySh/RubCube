using System.Collections.Generic;

namespace SA.CrossPlatform.Analytics
{
    /// <summary>
    /// Analytics client interface.
    /// Contain methods that allow report application events to the analytics server.
    /// </summary>
    public interface UM_IAnalyticsClient
    {
        /// <summary>
        /// Init Analytics client, should be called as early as possible on app launch.
        /// </summary>
        void Init();

        /// <summary>
        /// Returns <c>true</c> in case client is initialized, otherwise <c>false</c>.
        /// </summary>
        bool IsInitialized { get; }

        /// <summary>
        /// Send an analytics event.
        /// </summary>
        /// <param name="eventName">custom event name</param>
        void Event(string eventName);

        /// <summary>
        /// Send an analytics event.
        /// </summary>
        /// <param name="eventName">custom event name</param>
        /// <param name="data">custom event data</param>
        void Event(string eventName, IDictionary<string, object> data);

        /// <summary>
        /// Tracking Monetization (optional).
        /// </summary>
        /// <param name="productId">The id of the purchased item.</param>
        /// <param name="amount">The price of the item.</param>
        /// <param name="currency">
        ///  Abbreviation of the currency used for the transaction. For example “USD” (United
        ///  States Dollars). See http:en.wikipedia.orgwikiISO_4217 for a standardized list
        ///  of currency abbreviations.
        /// </param>
        void Transaction(string productId, float amount, string currency);

        /// <summary>
        ///  User Demographics (optional).
        /// </summary>
        /// <param name="userId">User unique id</param>
        void SetUserId(string userId);

        /// <summary>
        /// User Demographics (optional).
        /// </summary>
        /// <param name="birthYear">Birth year of user. Must be 4-digit year format, only.</param>
        void SetUserBirthYear(int birthYear);

        /// <summary>
        /// User Demographics (optional).
        /// </summary>
        /// <param name="gender">Gender of a user</param>
        void SetUserGender(UM_Gender gender);
    }
}
