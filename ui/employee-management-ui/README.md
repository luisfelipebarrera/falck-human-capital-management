# Employee Management Ui

Project structure

```bash

core
├── authentication
│   ├── authentication.service.ts
│   ├── token-storage.service.ts
│   └── auth.service.ts
│
├── guards
│   └── auth.guard.ts
│
├── interceptors
│   └── auth.interceptor.ts
│
├── models
│   ├── login-request.ts
│   ├── login-response.ts
│   ├── user.ts
│   └── api-error.ts
│
├── services
│
├── constants
│   ├── api.constants.ts
│   ├── roles.ts
│   └── storage.constants.ts
│
└── utils

```

## Development server

```bash

# Check versions
npm list @angular/core
npm list rxjs
npm list bootstrap

```

To start a local development server, run:

```bash
ng serve
```

## Dependencies

```bash
npm install
npm install rxjs
npm install jwt-decode
```
