"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var SignUpRequest = (function () {
    function SignUpRequest(firstName, lastName, email, password, confirmPassword, verificationCode) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.password = password;
        this.confirmPassword = confirmPassword;
        this.verificationCode = verificationCode;
    }
    return SignUpRequest;
}());
exports.SignUpRequest = SignUpRequest;
//# sourceMappingURL=signUpRequest.js.map