import { LOGIN_URL, REGISTER_URL } from "~/utils/config/api/endpoints";
import HttpFactory from "~/repository/factory";
import { HttpMethod } from "~/models/enums/httpMethod";
import type { LoginResponse } from "~/models/payloads/loginResponse";
import type { RegisterSchema } from "~/models/schemas/auth/register";
import type {LoginSchema} from "~/models/schemas/auth/login";


class AuthModule extends HttpFactory {
    async create(data: RegisterSchema) {
        return await this.call(HttpMethod.POST, REGISTER_URL, data);
    }

    async login(data: LoginSchema): Promise<LoginResponse> {
        return await this.call<LoginResponse>(HttpMethod.POST, `${LOGIN_URL}`, data);
    }

    async socialSignIn(provider: string, user: any): Promise<LoginResponse> {
        return await this.call<LoginResponse>(HttpMethod.POST, `${LOGIN_URL}/social/${provider}`, user);
    }
}

export default AuthModule;
