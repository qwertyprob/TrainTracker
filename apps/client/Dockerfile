FROM node:20-alpine AS builder
WORKDIR /app

RUN corepack enable && corepack prepare pnpm@latest --activate

# Копируем lock + package.json
COPY pnpm-lock.yaml package.json ./
RUN pnpm install --frozen-lockfile

# Копируем исходники + env в контейнер
COPY . .

# Build env уже есть внутри контейнера
ENV NEXT_TELEMETRY_DISABLED=1
RUN pnpm build

# RUN stage
FROM node:20-alpine AS runner
WORKDIR /app

ENV NODE_ENV=production
ENV NEXT_TELEMETRY_DISABLED=1

COPY --from=builder /app/.next/standalone ./
COPY --from=builder /app/.next/static ./.next/static
COPY --from=builder /app/public ./public

EXPOSE 3000
CMD ["node", "server.js"]
