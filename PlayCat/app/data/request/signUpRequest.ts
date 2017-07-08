export class SignUpRequest {
    constructor(firstName: string,
        lastName: string,
        email: string,
        password: string,
        confirmPassword: string,
        verificationCode: string) {        
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.password = password;        
        this.confirmPassword = confirmPassword;
        this.verificationCode = verificationCode;
    }

    firstName: string;
    lastName: string;
    email: string;
    password: string;
    confirmPassword: string;
    verificationCode: string;
}