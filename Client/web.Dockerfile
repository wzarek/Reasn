FROM node:21.7.1-alpine as base

FROM base as prunner
RUN apk add --no-cache libc6-compat
copy reasn-client /source
WORKDIR /source
RUN yarn set version 4.1.0 \ 
    && yarn dlx turbo prune web --docker

FROM base AS installer
RUN apk add --no-cache libc6-compat
WORKDIR /source 
COPY --from=prunner /source/out/json/ .
COPY --from=prunner /source/out/yarn.lock ./yarn.lock
RUN --mount=type=cache,target=.yarn/cache \
    yarn install --immutable
COPY --from=prunner /source/out/full .

FROM installer AS development
ARG NODE_ENV=development
ENV NODE_ENV=${NODE_ENV}
CMD ["yarn", "run", "dev:web"]
