import { z } from "zod";

export const CommentRequestSchema = z.object({
  content: z.string().max(1024),
});

export type CommentRequest = z.infer<typeof CommentRequestSchema>;
