import HttpFactory from "~/repository/factory";
import {HttpMethod} from "~/models/enums/httpMethod";
import {SUBSCRIBE_NEWSLETTER_URL} from "~/utils/config/api/endpoints";

class NewsletterModule extends HttpFactory {
    async subscribe({email}: {email: string}) {
        return await this.call<void>(HttpMethod.POST, SUBSCRIBE_NEWSLETTER_URL, {email});
    }
}

export default NewsletterModule;