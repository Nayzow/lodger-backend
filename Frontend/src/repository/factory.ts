import type {$Fetch} from "nitropack";
import type {HttpMethod} from "~/models/enums/httpMethod";

type RequestInterceptor = ({url, options}: {url: string, options: any}) => any;

class HttpFactory {
    private readonly $fetch: $Fetch;
    private readonly requestInterceptor: RequestInterceptor[] = [];

    constructor(fetcher: $Fetch) {
        this.$fetch = fetcher;
    }

    addRequestInterceptor(interceptor: RequestInterceptor) {
        this.requestInterceptor.push(interceptor);
    }

    async call<T>(method: HttpMethod, url: string, data?: object, extras = {}): Promise<T> {
        let options = { method, body: data, ...extras };

        for (const interceptor of this.requestInterceptor) {
            options = interceptor({url, options});
        }

        return await this.$fetch(url, {method, body: data, ...extras});
    }
}

export default HttpFactory;