import { TokenModel } from "../models/auth/token-model";
import { TokenViewModel } from "../models/auth/token-viewmodel";

export function toTokenModel(tokenViewModel: TokenViewModel) : TokenModel {
    const tokenModel:TokenModel = new TokenModel();

    tokenModel.token = tokenViewModel.token;
    tokenModel.roles = tokenViewModel.roles;

    const today = new Date();
    today.setDate(today.getDate() + tokenViewModel.expirationDays);
    tokenModel.expirationDate = today;

    return tokenModel;
}