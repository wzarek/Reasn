import {
  Button,
  H1,
  Paragraph,
  Separator,
  YStack,
} from '@my/ui'
import { useLink } from 'solito/link'

export const HomeScreen = () => {
  const link = useLink({
    href: '/user/420',
  })

  return (
    <YStack f={1} jc="center" ai="center" p="$4" gap>
      <YStack gap="$4" bc="$background">
        <H1 ta="center">Reasn.</H1>
        <Separator />
        <Paragraph>find your reasn to meet</Paragraph>
        <Button {...link}>
          user
        </Button>
      </YStack>
    </YStack>
  )
}
