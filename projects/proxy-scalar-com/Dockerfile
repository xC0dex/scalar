# Build image
FROM golang:1.22-alpine as build
WORKDIR /app

COPY ./main.go ./
COPY ./public ./public

RUN go build main.go

# Build distroless image
FROM gcr.io/distroless/static-debian11
WORKDIR /app

ENV PORT=9033

# Copy the compiled app binary and public directory
COPY --from=build --chown=1001:1001 /app/main /app/
COPY --from=build --chown=1001:1001 /app/public /app/public

# Run as non-root user
USER 1001:1001

CMD [ "/app/main" ]
