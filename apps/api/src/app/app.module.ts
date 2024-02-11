import { monitor } from '@colyseus/monitor';
import { MiddlewareConsumer, Module, NestModule } from '@nestjs/common';
import { ThrottlerModule } from '@nestjs/throttler';
import graphqlUploadExpress from 'graphql-upload/graphqlUploadExpress.js';

import { environment } from '../environments/environment';
import { ZenAuthModule } from './auth';
import { ConfigModule, ConfigService } from './config';
import { ToolsController } from './controllers';
import { GameService } from './game';
import { ZenGraphQLModule } from './graphql';
import { JwtModule } from './jwt';
import { MailModule } from './mail';
import { PrismaModule } from './prisma';

@Module({
  imports: [
    ThrottlerModule.forRootAsync({
      imports: [ConfigModule],
      inject: [ConfigService],
      useFactory: (config: ConfigService) => config.throttle,
    }),
    ZenAuthModule,
    ConfigModule,
    ZenGraphQLModule,
    JwtModule,
    MailModule,
    PrismaModule,
  ],
  providers: [GameService],
  controllers: [ToolsController],
})
export class AppModule implements NestModule {
  constructor(private readonly config: ConfigService) {}

  configure(consumer: MiddlewareConsumer) {
    consumer.apply(graphqlUploadExpress(environment.graphql.uploads)).forRoutes('graphql');

    if (this.config.colyseus?.monitor) consumer.apply(monitor()).forRoutes('/monitor');
  }
}
