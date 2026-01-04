import React, { useEffect, useState } from 'react'
import { View, Text, TouchableOpacity, ActivityIndicator } from 'react-native'
import { checkTripStatus, startTrip } from '@/services/api'

type Props = {
  tripId?: string | null
  status?: number | null
  children?: React.ReactNode
}

const TripCard = ({ tripId, status = 0, children }: Props) => {
  let borderClass = 'border-transparent'
  const [pressing, setPressing] = useState(false)
  const [tripStatus, setTripStatus] = useState<number | null>(status ?? null)
  const [checking, setChecking] = useState(false)

  useEffect(() => {
    let mounted = true
    const fetchStatus = async () => {
      if (!tripId) return
      try {
        setChecking(true)
        const res = await checkTripStatus(tripId)
        if (!mounted) return
        if (typeof res === 'number') setTripStatus(res)
        else if (res && typeof res.status === 'number') setTripStatus(res.status)
        else console.warn('Unexpected checkTripStatus response', res)
      } catch (err) {
        console.error('Failed to fetch trip status', err)
      } finally {
        if (mounted) setChecking(false)
      }
    }
    fetchStatus()
    return () => { mounted = false }
  }, [tripId])

  switch (status) {
    case 1:
      borderClass = 'border-2 border-yellow-400'
      break
    case 2:
      borderClass = 'border-2 border-green-500'
      break
    case 3:
      borderClass = 'border-2 border-blue-500'
      break
    case 4:
      borderClass = 'border-2 border-red-500'
      break
    default:
      borderClass = 'border border-transparent'
  }

  return (
    <View className="items-center mb-4">
      <View className={`w-[90%] rounded-lg p-2 ${borderClass} relative`}>
        {tripId ? (
          <Text className="text-white text-sm font-semibold mb-2">Trip: {tripId}</Text>
        ) : null}

        <View>{children}</View>

        {tripId && tripStatus === 0 ? (
          <TouchableOpacity
            className="absolute right-3 bottom-3 bg-blue-600 rounded-full px-3 py-2 items-center justify-center"
            onPress={async () => {
              if (!tripId) return
              try {
                setPressing(true)
                await startTrip(tripId)
                setTripStatus(1)
              } catch (err) {
                console.error('Failed to start trip', err)
              } finally {
                setPressing(false)
              }
            }}
          >
            {pressing ? (
              <ActivityIndicator color="#fff" />
            ) : (
              <Text className="text-white font-semibold text-sm">Start trip</Text>
            )}
          </TouchableOpacity>
        ) : null}
      </View>
    </View>
  )
}

export default TripCard
