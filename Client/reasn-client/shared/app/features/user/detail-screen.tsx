import { Button, H1, Paragraph, Separator, YStack } from '@my/ui'
import { ChevronLeft } from '@tamagui/lucide-icons'
import { createParam } from 'solito'
import { useLink } from 'solito/link'

const { useParam } = createParam<{ id: string }>()

export const UserDetailScreen = () => {
  const [id] = useParam('id')
  const link = useLink({
    href: '/',
  })

  return (
    <YStack f={1} jc="center" ai="center" p="$4" gap>
      <YStack gap="$4" bc="$background">
        <H1 ta="center">Reasn.</H1>
        <Separator />
        <Paragraph ta="center">{`User ID: ${id}`}</Paragraph>
        <Button {...link} icon={ChevronLeft}>
          back
        </Button>
      </YStack>
    </YStack>
  )
}
